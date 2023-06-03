using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scraper.Domain.Entities;

namespace Scraper.Infrastructure.Persistence.Configurations.Application
{
    public class OrderEventConfiguration : IEntityTypeConfiguration<OrderEvent>
    {
        public void Configure(EntityTypeBuilder<OrderEvent> builder)
        {
            //Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //OrderStatus
            builder.Property(x => x.Status).IsRequired();

            //CreatedOn
            builder.Property(x => x.CreatedOn).IsRequired();

            //Create Table
            builder.ToTable("OrderEvents");

        }
    }
}
