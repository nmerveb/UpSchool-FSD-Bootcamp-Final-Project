using Microsoft.AspNetCore.Mvc;
using Scraper.Application.Features.Products.Queries.GetAll;

namespace Scraper.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ApiControllerBase
    {
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAllAsync(ProductsGetAllQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
