using Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class HoldingsContext : DbContext
    {
        public HoldingsContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Portfolio> Portfolios { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Holding> Holdings { get; set; }
    }
}