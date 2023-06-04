using Scraper.Console;
using Scraper.Domain.Entities;

namespace Scraper.Application.Common.Models
{
    public class OrderResponseDto
    {
        public List<Product> Products { get; set; }

        public List<OrderEvent> OrderEvents { get; set; }

        public int TotalProductAmount { get; set; }

        public OrderResponseDto()
        {
            Products = new List<Product>();
            OrderEvents = new List<OrderEvent>();
        }
        public List<Product> MapToProduct (List<ProductDto> productDtos)
        {
            foreach(var productDto in productDtos)
            {
                var product = new Product
                {
                    OrderId = productDto.OrderId,
                    Name = productDto.Name,
                    Picture = productDto.Picture,
                    IsOnSale = productDto.IsOnSale,
                    Price = productDto.Price,
                    SalePrice = !string.IsNullOrEmpty(productDto.SalePrice) ? productDto.SalePrice : null
                };

                Products.Add(product);
            }

            return Products;
        }

        public List<OrderEvent> MapToOrderEvent(List<OrderEventDto> orderEventDtos)
        {
            foreach (var orderEventDto in orderEventDtos)
            {
                var orderEvent = new OrderEvent
                {
                    OrderId = orderEventDto.OrderId,
                    Status = (Domain.Enums.OrderStatus)orderEventDto.Status,
                    CreatedOn = orderEventDto.CreatedOn,
                };

                OrderEvents.Add(orderEvent);
            }

            return OrderEvents;
        }
    }
}
