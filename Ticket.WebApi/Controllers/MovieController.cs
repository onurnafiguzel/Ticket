using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Ticket.Business.Abstract;
using Ticket.Business.Models;
using Ticket.Domain.Dtos;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService filmService;

        public MovieController(IMovieService filmService)
        {
            this.filmService = filmService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery)
        {
            var result = await filmService.GetAll(paginationQuery);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        // [HttpGet("{int:filmId}")]
        [HttpGet("{filmId:int}")]
        public async Task<IActionResult> Get(int filmId)
        {
            var result = await filmService.Get(filmId);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetMovieBySlug(string slug)
        {
            var result = await filmService.GetBySlug(slug);
            if (result.Success)
            {             
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("{slug}/similar_movies")]
        public async Task<IActionResult> GetSimiliarFilms(string slug)
        {
            var result = await filmService.GetSimiliarMovies();
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("{slug}/cast")]
        public async Task<IActionResult> GetCastsByMovie(string slug)
        {
            var result = await filmService.GetCastsByMovie(slug);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("{slug}/sessions")]
        public async Task<IActionResult> GetMovieSessions(string slug, [FromQuery] [Required] int city, [FromQuery] DateTime date)
        {
            var result = await filmService.GetMovieSessions(slug, city, date);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Movie film)
        {
            var result = await filmService.Add(film);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Movie film)
        {
            var result = await filmService.Update(film);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int filmId)
        {
            var result = await filmService.Delete(filmId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
