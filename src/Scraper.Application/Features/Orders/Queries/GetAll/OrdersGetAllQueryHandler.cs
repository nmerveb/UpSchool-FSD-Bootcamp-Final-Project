using MediatR;
using Microsoft.EntityFrameworkCore;
using Scraper.Application.Common.Interfaces;

namespace Scraper.Application.Features.Orders.Queries.GetAll
{
    public class OrdersGetAllQueryHandler : IRequestHandler<OrdersGetAllQuery, List<OrdersGetAllDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public OrdersGetAllQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<OrdersGetAllDto>> Handle(OrdersGetAllQuery request, CancellationToken cancellationToken)
        {
           var orderDtos = await _applicationDbContext.Orders
                .Where(x => x.UserId == request.User)
                .Select(x => new OrdersGetAllDto(x.Id, x.RequestedAmount, x.TotalFoundAmount, x.ScrapingType, x.CreatedOn))
                .ToListAsync(cancellationToken);

            return orderDtos;
        }
    }
}
