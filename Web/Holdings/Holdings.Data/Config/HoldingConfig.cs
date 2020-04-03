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
                .HasKey(m => m.HoldingId);

            builder
                .Property(m => m.HoldingId)
                .UseIdentityColumn();

            builder
                .Property(m => m.AssetClass)
                .IsRequired();

            builder
                .Property(m => m.Symbol)
                .IsRequired();

            builder
                .Property(m => m.Quantity)
                .IsRequired();

            builder
                .Property(m => m.BuyPrice)
                .HasColumnType("DECIMAL(18,4)")
                .IsRequired();

            builder
                .Property(m => m.Description)
                .HasMaxLength(500);

            builder
                .ToTable("Holding");
        }
    }
}