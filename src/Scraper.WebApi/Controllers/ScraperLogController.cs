using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Scraper.Console;
using Scraper.WebApi.Hubs;

namespace Scraper.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScraperLogController : ControllerBase
    {
        private readonly IHubContext<ScraperLogHub> _scraperLogHubContext;

        public ScraperLogController(IHubContext<ScraperLogHub> scraperLogHubContext)
        {
            _scraperLogHubContext = scraperLogHubContext;
        }

        //[HttpPost]
        //public async Task<IActionResult> SendLogNotificationAsync(ScraperLogDto logApiDto)
        //{
        //    await _scraperLogHubContext.Clients.AllExcept(logApiDto.ConnectionId).SendAsync("NewScraperLogAdded", logApiDto.Message, logApiDto.CreatedOn);

        //    return Ok();
        //}
    }
}
