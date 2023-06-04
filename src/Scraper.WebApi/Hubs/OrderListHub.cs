using Microsoft.AspNetCore.SignalR;

namespace Scraper.WebApi.Hubs
{
    public class OrderListHub : Hub
    {
        public async Task AddOrderAsync(string orderId)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("NewOrderAdded", orderId);
        }
    }
}
