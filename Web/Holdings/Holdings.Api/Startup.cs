using Holdings.Core.Repositories.Contracts;
using Holdings.Core.Services.Contracts;
using Holdings.Data;
using Holdings.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AutoMapper;

namespace Holdings.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<HoldingsDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("Holdings.Data")));

            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IPortfolioService, PortfolioService>();
            services.AddTransient<IModelService, ModelService>();
            services.AddTransient<IHoldingService, HoldingService>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v01", new OpenApiInfo { Title = "Holdings", Version = "v0.1" });
            });

            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v01/swagger.json", "Holdings v0.1");
            });
        }
    }
}