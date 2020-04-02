using Holdings.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Holdings.Data.Config
{
    public class HoldingConfig : IEntityTypeConfiguration<Holding>
    {
        public void Configure(EntityTypeBuilder<Holding> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

            builder
                .Property(m => m.HoldingType)
                .IsRequired();

            builder
                .Property(m => m.Symbol)
                .IsRequired();

            builder
                .Property(m => m.Quantity)
                .IsRequired();

            builder
                .Property(m => m.BuyPrice)
                .IsRequired();

            builder
                .Property(m => m.Description)
                .HasMaxLength(500);
             

            builder
                .ToTable("Holding");
        }
    }
}