using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Ticket.Business.Abstract;
using Ticket.Business.Models;

namespace Ticket.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IMovieService filmService;

        public SearchController(IMovieService filmService)
        {
            this.filmService = filmService;
        }

        //TODO FULLTEXTSEARCH MİKTARA GÖRE SIRALAMAS
        [HttpGet("movie")] // QueryParameter olacak.
        public async Task<IActionResult> GetMoviesBySearch([FromQuery] [MinLength(3)] string q, [FromQuery] PaginationQuery paginationQuery)
        {
            var result = await filmService.GetMoviesBySearch(q, paginationQuery);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
