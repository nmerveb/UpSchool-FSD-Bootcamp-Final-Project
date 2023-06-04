using Microsoft.AspNetCore.Mvc;
using Scraper.Application.Features.OrderEvents.Queries.GetAll;

namespace Scraper.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderEventsController : ApiControllerBase
    {
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAllAsync(OrderEventsGetAllQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
