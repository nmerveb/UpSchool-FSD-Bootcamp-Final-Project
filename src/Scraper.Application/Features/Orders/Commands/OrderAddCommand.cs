using MediatR;
using Scraper.Domain.Common;
using Scraper.Domain.Enums;

namespace Scraper.Application.Features.Orders.Commands
{
    public class OrderAddCommand : IRequest<Response<Guid>>
    {
        public string RequestedAmount { get; set; }
        public ScrapingType ScrapingType { get; set; }

    }

}
