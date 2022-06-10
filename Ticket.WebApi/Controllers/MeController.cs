using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ticket.Business.Abstract;
using Ticket.Business.Models;

namespace Ticket.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MeController : ControllerBase
    {
        private readonly ICustomerService userService;
        private readonly ITicketService ticketService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MeController(ICustomerService userService, ITicketService ticketService, IHttpContextAccessor httpContextAccessor)
        {
            this.userService = userService;
            this.ticketService = ticketService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Me()
        {
            var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await userService.Get(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("tickets")]
        public async Task<IActionResult> Tickets([FromQuery] PaginationQuery paginationQuery)
        {
            var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var tickets = await ticketService.GetAll(paginationQuery, userId);
            if (!tickets.Success)
            {
                return BadRequest(tickets);
            }

            return Ok(tickets);
        }
    }
}
