using MediatR;
using Microsoft.EntityFrameworkCore;
using Scraper.Application.Common.Interfaces;
using Scraper.Application.Features.OrderEvents.Queries.GetAll;
using Scraper.Domain.Entities;

namespace Scraper.Application.Features.Products.Queries.GetAll
{
    public class ProductsGetAllQueryHandler : IRequestHandler<ProductsGetAllQuery, List<ProductsGetAllDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ProductsGetAllQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<ProductsGetAllDto>> Handle(ProductsGetAllQuery request, CancellationToken cancellationToken)
        {
            var products = await _applicationDbContext.Products
                 .Where(x => x.OrderId == request.OrderId)
                 .ToListAsync(cancellationToken);

            var productDtos = MapProductToProductsDto(products);

            return productDtos;
        }

        private List<ProductsGetAllDto> MapProductToProductsDto(List<Product> products)
        {
            List<ProductsGetAllDto> productsGetAllDtos = new List<ProductsGetAllDto>();

            foreach (var product in products)
            {
                var productsDto = new ProductsGetAllDto()
                {
                    OrderId = product.OrderId,
                    Name = product.Name,
                    Picture = product.Picture,
                    IsOnSale = product.IsOnSale,
                    Price = product.Price,
                    SalePrice = product.SalePrice

                };

                productsGetAllDtos.Add(productsDto);
            }

            return productsGetAllDtos;
        }

    }
}
