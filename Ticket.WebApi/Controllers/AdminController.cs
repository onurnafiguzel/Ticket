using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Utilities.Results;
using Ticket.Business.Abstract;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpGet]
        public async Task<IDataResult<IList<Admin>>> GetAll()
        {
            var result = await adminService.GetAll();
            if (result.Success)
            {
                return result;
            }
            return null;
        }
    }
}
