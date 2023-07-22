using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scraper.Application.Features.Orders.Commands;
using Scraper.Application.Features.Orders.Queries.GetAll;

namespace Scraper.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ApiControllerBase
    {
        [HttpPost("CreateOrder")]
        public  async Task<IActionResult> CreateOrder(OrderAddCommand command )
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAllAsync(OrdersGetAllQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
