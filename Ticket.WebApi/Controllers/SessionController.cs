using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ticket.Application.Utilities.IoC;
using Ticket.Business.Abstract;
using Ticket.Business.Models;
using Ticket.Data.Abstract;
using Ticket.Domain.Dtos;

namespace Ticket.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private ISessionService service;
        private IHttpContextAccessor _httpContextAccessor;

        public SessionController(ISessionService service)
        {
            this.service = service;
            this._httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await service.GetSession(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery, [FromQuery] int theatherId)
        {
            var result = await service.GetAll(paginationQuery, theatherId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize]
        [HttpPost("{id}/buy")]
        public async Task<IActionResult> Buy(int id, [FromBody] SessionBuyDto body)
        {
            var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await service.TryBuy(id, userId, body);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
