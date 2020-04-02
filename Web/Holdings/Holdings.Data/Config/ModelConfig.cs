using Holdings.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Holdings.Data.Config
{
    public class ModelConfig : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder
                .HasKey(m => m.ModelId);

            builder
                .Property(m => m.ModelId)
                .UseIdentityColumn();

            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(m => m.Description)
                .HasMaxLength(500);

            builder
                .HasMany(c => c.Holdings)
                .WithOne(u => u.Model);

            builder
                .ToTable("Model");
        }
    }
}