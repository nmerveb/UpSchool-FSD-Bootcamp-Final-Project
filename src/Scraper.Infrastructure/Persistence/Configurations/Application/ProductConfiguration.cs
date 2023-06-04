using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scraper.Domain.Entities;

namespace Scraper.Infrastructure.Persistence.Configurations.Application
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            //Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //Name
            builder.Property(x => x.Name).IsRequired();

            //Picture
            builder.Property(x => x.Picture).IsRequired();

            //IsOnSale
            builder.Property(x => x.IsOnSale).IsRequired();

            //Price
            builder.Property(x => x.Price).IsRequired();

            //Sale Price
            builder.Property(x => x.SalePrice).IsRequired(false);


            //Create table
            builder.ToTable("Products");

        }
    }
}
