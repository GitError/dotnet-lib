using Holdings.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Holdings.Data.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(m => m.UserId);

            builder
               .Property(m => m.UserId)
               .UseIdentityColumn();

            builder
               .Property(m => m.Username)
               .IsRequired();

            builder
                .HasMany(c => c.Portfolios)
                .WithOne(u => u.User);
             
            builder
                .ToTable("User");
        }
    }
}