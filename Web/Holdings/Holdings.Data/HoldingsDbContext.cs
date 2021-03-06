﻿using Holdings.Core.Models;
using Holdings.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace Holdings.Data
{
    public class HoldingsDbContext : DbContext
    {
        public DbSet<Portfolio> Portfolios { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Holding> Holdings { get; set; }

        public DbSet<User> Users { get; set; }

        public HoldingsDbContext(DbContextOptions<HoldingsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .ApplyConfiguration(new PortfolioConfig())
                .ApplyConfiguration(new ModelConfig())
                .ApplyConfiguration(new HoldingConfig())
                .ApplyConfiguration(new UserConfig());
        }
    }
}