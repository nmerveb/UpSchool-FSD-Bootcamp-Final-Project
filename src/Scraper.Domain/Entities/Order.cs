using Scraper.Domain.Common;
using Scraper.Domain.Enums;
using Scraper.Domain.Identity;

namespace Scraper.Domain.Entities
{
    public class Order : EntityBase<Guid>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string RequestedAmount { get; set; } //Istenen urun
        public int TotalFoundAmount { get; set; } //bulunan urun
        public ScrapingType ScrapingType { get; set; }
        public ICollection<OrderEvent> OrderEvents { get; set; } //Bot started
        public ICollection<Product> Products { get; set; }
        public DateTimeOffset CreatedOn { get; set; }


        public Order()
        {
            OrderEvents = new List<OrderEvent>();
            Products = new List<Product>();
            User = new User();
        }
    }

}
