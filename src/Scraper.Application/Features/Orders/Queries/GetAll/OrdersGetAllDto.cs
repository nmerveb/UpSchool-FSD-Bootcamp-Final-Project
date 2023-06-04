using Scraper.Domain.Enums;

namespace Scraper.Application.Features.Orders.Queries.GetAll
{
    public class OrdersGetAllDto
    {
        public Guid Id { get; set; }
        public string RequestedAmount { get; set; } 
        public int TotalFoundAmount { get; set; } 
        public ScrapingType ScrapingType { get; set; }
        public DateTimeOffset CreatedOn { get; set; }

        public OrdersGetAllDto(Guid id, string requestedAmount, int totalFoundAmount, ScrapingType scrapingType, DateTimeOffset createdOn)
        {
            Id = id;
            RequestedAmount = requestedAmount;
            TotalFoundAmount = totalFoundAmount;
            ScrapingType = scrapingType;
            CreatedOn = createdOn;
        }
    }
}
