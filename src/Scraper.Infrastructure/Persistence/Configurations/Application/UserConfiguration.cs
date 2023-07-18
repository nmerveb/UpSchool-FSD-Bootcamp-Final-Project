using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scraper.Domain.Entities;

namespace Scraper.Infrastructure.Persistence.Configurations.Application
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // UserId
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.UserId).ValueGeneratedOnAdd();

            // FirstName
            builder.Property(x => x.FirstName).IsRequired();

            // LastName
            builder.Property(x => x.LastName).IsRequired();

            // Email
            builder.Property(x => x.Email).IsRequired();

            // Orders
            builder.HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            // Create table
            builder.ToTable("Users");
        }
    }
}
