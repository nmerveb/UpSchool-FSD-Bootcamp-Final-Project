using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Scraper.Application.Common.Models;
using Scraper.WebApi.Hubs;

namespace Scraper.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderListController : ControllerBase
    {
        private readonly IHubContext<OrderListHub> _orderListHubContext;

        public OrderListController(IHubContext<OrderListHub> orderListHubContext)
        {
            _orderListHubContext = orderListHubContext;
        }

        [HttpPost]
        public async Task<IActionResult> SendLogNotificationAsync(AddOrderDto logApiDto)
        {
            await _orderListHubContext.Clients.AllExcept(logApiDto.ConnectionId).SendAsync("NewOrderAdded", logApiDto.Id);

            return Ok();
        }

    }
}
