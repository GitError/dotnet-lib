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
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(m => m.Description)
                .HasMaxLength(500);

            builder
               .HasMany(x => x.Holdings)
               .WithOne(x => x.Model);

            builder
                .ToTable("Model");
        }
    }
}