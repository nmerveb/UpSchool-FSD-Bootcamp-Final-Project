using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scraper.Domain.Entities;

namespace Scraper.Infrastructure.Persistence.Configurations.Application
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            //Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //RequestedAmount
            builder.Property(x => x.RequestedAmount).IsRequired();

            //TotalFoundAmount
            builder.Property(x => x.TotalFoundAmount).IsRequired();

            //ScrapingType
            builder.Property(x => x.ScrapingType).IsRequired();

            //CreatedOn
            builder.Property(x => x.CreatedOn).IsRequired();


            //OrderEvents
            builder.HasMany(o => o.OrderEvents)
                .WithOne(e => e.Order)
                .HasForeignKey(e => e.OrderId); 

            //Products
            builder.HasMany(o => o.Products)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId);

            //UserId
            builder.HasIndex(order => order.UserId);

            // User ile Order arasında One-to-Many ilişkiyi tanımlayın
            builder.HasOne(order => order.User)
                   .WithMany(user => user.Orders)
                   .HasForeignKey(order => order.UserId)
                   .IsRequired();

            //Create table
            builder.ToTable("Orders");

        }
    }
}
