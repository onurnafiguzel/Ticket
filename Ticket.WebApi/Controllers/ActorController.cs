using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Business.Abstract;
using Ticket.Business.Models;

namespace Ticket.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService actorService;

        public ActorController(IActorService actorService)
        {
            this.actorService = actorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery, string? q)
        {
            var result = await actorService.GetAll(paginationQuery, q);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
