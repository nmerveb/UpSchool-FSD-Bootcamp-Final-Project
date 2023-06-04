using MediatR;

namespace Scraper.Application.Features.OrderEvents.Queries.GetAll
{
    public class OrderEventsGetAllQuery : IRequest<List<OrderEventsGetAllDto>>
    {
        public Guid OrderId { get; set; }
    }
}
