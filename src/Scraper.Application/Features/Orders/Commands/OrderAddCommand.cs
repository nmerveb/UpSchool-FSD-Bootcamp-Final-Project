using MediatR;
using Scraper.Domain.Common;
using Scraper.Domain.Enums;

namespace Scraper.Application.Features.Orders.Commands
{
    public class OrderAddCommand : IRequest<Response<Guid>>
    {
        public string User { get; set; }
        public string AccessToken { get; set; }
        public string RequestedAmount { get; set; }
        public ScrapingType ScrapingType { get; set; }

    }

}
