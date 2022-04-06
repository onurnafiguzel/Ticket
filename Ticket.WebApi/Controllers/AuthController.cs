using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Business.Abstract;
using Ticket.Domain.Dtos;

namespace Ticket.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
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

        [HttpPost]
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
