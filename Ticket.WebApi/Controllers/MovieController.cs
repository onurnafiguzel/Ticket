﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Business.Abstract;
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
        public async Task<IActionResult> GetAll()
        {
            var result = await filmService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{filmId}")]
        public async Task<IActionResult> Get(int filmId)
        {
            var result = await filmService.Get(filmId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
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