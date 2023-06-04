using MediatR;

namespace Scraper.Application.Features.Orders.Queries.GetAll
{
    public class OrdersGetAllQuery : IRequest<List<OrdersGetAllDto>>
    {

    }
}
