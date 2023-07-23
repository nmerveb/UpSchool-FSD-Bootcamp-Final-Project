using MediatR;
using Scraper.Application.Common.Interfaces;
using Scraper.Application.Common.Models;
using Scraper.Application.Utils;
using Scraper.Domain.Common;
using Scraper.Domain.Entities;
using Scraper.Domain.Enums;

namespace Scraper.Application.Features.Orders.Commands
{
    public class OrderAddCommandHandler :  IRequestHandler<OrderAddCommand, Response<Guid>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        
        private OrderResponseDto _responseDto;
        public OrderAddCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _responseDto = new OrderResponseDto();
        }

        public async Task<Response<Guid>> Handle(OrderAddCommand request, CancellationToken cancellationToken)
        {

            var crawler = new Crawler();

            Guid orderId = Guid.NewGuid();
            Order addOrder =  new Order();
            
            var response = await  crawler.ScrapProducts(orderId);
            
            var onDiscount = response.Products.Where(x => x.IsOnSale ==  true).ToList();
            var nonDiscount = response.Products.Where(x => x.IsOnSale == false).ToList();

            int requestedAmount = 0;

            if (request.RequestedAmount.Any() && (int.TryParse(request.RequestedAmount, out requestedAmount) || request.RequestedAmount == "All"))
            {

                switch (request.ScrapingType)
                {
                    case ScrapingType.All:
                        addOrder.Products = requestedAmount != 0 ?
                            _responseDto.MapToProduct(response.Products.GetRange(0, requestedAmount))
                            : _responseDto.MapToProduct(response.Products);

                        break;

                    case ScrapingType.OnDiscount:
                        addOrder.Products = requestedAmount != 0 && requestedAmount< onDiscount.Count ?
                            _responseDto.MapToProduct(onDiscount.GetRange(0, requestedAmount))
                            : _responseDto.MapToProduct(onDiscount);
                        break;

                    case ScrapingType.NonDiscount:
                        addOrder.Products = requestedAmount != 0 && requestedAmount < nonDiscount.Count ?
                            _responseDto.MapToProduct(nonDiscount.GetRange(0, requestedAmount)) 
                            : _responseDto.MapToProduct(nonDiscount);
                        break;
                    default:
                        throw new NullReferenceException("scraping type is incorrect.");
                }

                addOrder.OrderEvents = _responseDto.MapToOrderEvent(response.OrderEvents);
            }

            addOrder.Id = orderId;
            addOrder.UserId = request.User;
            addOrder.CreatedOn = DateTimeOffset.Now;
            addOrder.TotalFoundAmount = response.TotalProductAmount;
            addOrder.RequestedAmount = request.RequestedAmount;
            addOrder.ScrapingType = request.ScrapingType;

            foreach(var orderProduct in addOrder.Products)
            {
                await _applicationDbContext.Products.AddAsync(orderProduct, cancellationToken);
            }

            foreach (var orderEvent in addOrder.OrderEvents)
            {
                await _applicationDbContext.OrderEvents.AddAsync(orderEvent, cancellationToken);
            }

            await _applicationDbContext.Orders.AddAsync(addOrder, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);


            return new Response<Guid>($"The new order \"{addOrder.Id}\" was succesfully added.", addOrder.Id);
        }

        
    }
}
