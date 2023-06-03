using Microsoft.EntityFrameworkCore;
using Scraper.Domain.Entities;

namespace Scraper.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Order> Orders { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<OrderEvent> OrderEvents { get; set; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        int SaveChanges();
    }
}
