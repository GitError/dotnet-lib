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
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

            builder
               .Property(m => m.UserName)
               .IsRequired();

            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .ToTable("Portfolio");
        }
    }
}