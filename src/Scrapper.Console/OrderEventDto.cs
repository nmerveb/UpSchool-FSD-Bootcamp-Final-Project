namespace Scraper.Console
{
    public class OrderEventDto
    {
        public Guid OrderId { get; set; }

        public OrderStatus Status { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public OrderEventDto(Guid orderId, OrderStatus status, DateTimeOffset createdOn)
        {
            OrderId = orderId;
            Status = status;
            CreatedOn = createdOn;

        }
    }
}
