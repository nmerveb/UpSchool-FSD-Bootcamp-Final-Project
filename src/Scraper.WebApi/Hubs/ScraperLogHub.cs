using Microsoft.AspNetCore.SignalR;
using Scraper.Console;

namespace Scraper.WebApi.Hubs
{
    public class ScraperLogHub : Hub
    {
        public async Task SendLogAsync(ScraperLogDto log)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("NewScraperLogAdded", log);
        }
    }
}
