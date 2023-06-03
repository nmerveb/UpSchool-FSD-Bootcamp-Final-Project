using Scraper.Domain.Common;
using Scraper.Domain.Enums;

namespace Scraper.Domain.Entities
{
    public class OrderEvent : EntityBase<Guid>
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public OrderStatus Status { get; set; }

    }
}
