using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Business.Abstract;
using Ticket.Business.Models;

namespace Ticket.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        private readonly ICastService castService;

        public CastController(ICastService castService)
        {
            this.castService = castService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery, string? q)
        {
            var result = await castService.GetAll(paginationQuery, q);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await castService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
