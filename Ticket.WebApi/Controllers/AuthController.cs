using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ticket.Application.Utilities.IoC;
using Ticket.Business.Abstract;
using Ticket.Domain.Dtos;

namespace Ticket.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService authService;
        private ICustomerService customerService;
        private IHttpContextAccessor _httpContextAccessor;

        public AuthController(IAuthService authService, ICustomerService customerService)
        {
            this.authService = authService;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            this.customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(CustomerForLoginDto customerForLoginDto)
        {
            var userToLogin = await authService.Login(customerForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin);
            }

            var result = await authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(CustomerForRegisterDto customerForRegisterDto)
        {
            var userExists = await authService.UserExists(customerForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists);
            }

            var registerResult = await authService.Register(customerForRegisterDto, customerForRegisterDto.Password);
            var result = await authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
