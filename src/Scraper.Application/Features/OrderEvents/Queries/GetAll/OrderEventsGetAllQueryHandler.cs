using MediatR;
using Microsoft.EntityFrameworkCore;
using Scraper.Application.Common.Interfaces;
using Scraper.Domain.Entities;

namespace Scraper.Application.Features.OrderEvents.Queries.GetAll
{
    public class OrderEventsGetAllQueryHandler : IRequestHandler<OrderEventsGetAllQuery, List<OrderEventsGetAllDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public OrderEventsGetAllQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<OrderEventsGetAllDto>> Handle(OrderEventsGetAllQuery request, CancellationToken cancellationToken)
        {
            var orderEvents = await _applicationDbContext.OrderEvents
                 .Where(x => x.OrderId.ToString() == request.OrderId)
                 .ToListAsync(cancellationToken);

            var orderEventDtos = MapOrderEventToOrderEventsDto(orderEvents);

            return orderEventDtos;
        }

        private List<OrderEventsGetAllDto> MapOrderEventToOrderEventsDto(List<OrderEvent> orderEvents)
        {
            List<OrderEventsGetAllDto> orderEventsGetAllDtos = new List<OrderEventsGetAllDto>();

            foreach (var orderEvent in orderEvents)
            {
                var orderEventsDto = new OrderEventsGetAllDto()
                {
                    OrderId = orderEvent.OrderId,
                    Status = orderEvent.Status,
                    CreatedOn = orderEvent.CreatedOn
                };

                orderEventsGetAllDtos.Add(orderEventsDto);
            }

            return orderEventsGetAllDtos;
        }
    }
}
