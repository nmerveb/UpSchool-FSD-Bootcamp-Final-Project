using Scraper.Domain.Common;
using Scraper.Domain.Enums;

namespace Scraper.Domain.Entities
{
    public class Order : EntityBase<Guid>
    {
        public Guid Id { get; set; }
        public int RequestedAmount { get; set; } //Istenen urun
        public int TotalFoundAmount { get; set; } //bulunan urun
        public ScrapingType ScrapingType { get; set; }
        public ICollection<OrderEvent> OrderEvents { get; set; } //Bot started
        public ICollection<Product> Products { get; set; }
        public DateTimeOffset CreatedOn { get; set; }

    }
}
