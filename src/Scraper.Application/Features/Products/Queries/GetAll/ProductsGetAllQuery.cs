using MediatR;

namespace Scraper.Application.Features.Products.Queries.GetAll
{
    public class ProductsGetAllQuery : IRequest<List<ProductsGetAllDto>>
    {
        public Guid OrderId { get; set; }
    }
}
