using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Business.Abstract;
using Ticket.Business.Models;

namespace Ticket.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        public readonly IPlaceService placeService;

        public PlaceController(IPlaceService placeService)
        {
            this.placeService = placeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery, [FromQuery] string? q, [FromQuery] int cityId)
        {
            var result = await placeService.GetAll(paginationQuery, q, cityId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await placeService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id}/theathers")]
        public async Task<IActionResult> Theathers(int id)
        {
            var result = await placeService.GetTheathers(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpGet("{id}/sessions")]
        public async Task<IActionResult> Sessions(int id, [FromQuery] DateTime date)
        {
            var result = await placeService.GetSessions(id, date);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
