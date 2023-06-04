using Scraper.Domain.Enums;

namespace Scraper.Application.Common.Models
{
    public class AddOrderDto
    {
        public Guid Id { get; set; }
        public string ConnectionId { get; set; }
    }
}
