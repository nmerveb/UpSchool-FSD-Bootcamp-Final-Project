using Scraper.Domain.Enums;

namespace Scraper.Application.Features.OrderEvents.Queries.GetAll
{
    public class OrderEventsGetAllDto
    {
        public Guid OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}
