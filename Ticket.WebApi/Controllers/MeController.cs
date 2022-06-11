using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ticket.Business.Abstract;
using Ticket.Business.Models;
using Ticket.Domain.Dtos;

namespace Ticket.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MeController : ControllerBase
    {
        private readonly ICustomerService userService;
        private readonly ITicketService ticketService;
        private readonly IAuthService authService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MeController(ICustomerService userService, ITicketService ticketService, IHttpContextAccessor httpContextAccessor, IAuthService authService)
        {
            this.userService = userService;
            this.ticketService = ticketService;
            _httpContextAccessor = httpContextAccessor;
            this.authService = authService;
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

        [HttpGet("details")]
        public async Task<IActionResult> Details()
        {
            var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await userService.Get(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize]
        [HttpPut("details")]
        public async Task<IActionResult> UpdateMe([FromBody] CustomerUpdateDto customerUpdateDto)
        {
            var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await userService.Get(userId);
            if (result.Success)
            {
                var newResult = await authService.Update(customerUpdateDto, userId);
                if (newResult.Success)
                {
                    return Ok(newResult);
                }
                return BadRequest(newResult);
            }
            return BadRequest(result);
        }
    }
}
