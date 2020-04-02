using Holdings.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Holdings.Data.Config
{
    public class PortfolioConfig : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder
                .HasKey(m => m.PortfolioId);

            builder
                .Property(m => m.PortfolioId)
                .UseIdentityColumn();

            builder
               .Property(m => m.Name)
               .IsRequired()
               .HasMaxLength(250);
             
            builder
               .HasMany(c => c.Models)
               .WithOne(u => u.Portfolio);
             
            builder
                .ToTable("Portfolio");
        }
    }
}