
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRental.API.Services;
using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieRental.API.Controllers
{
    [Route("api/movies")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService movieService;
        private readonly IMapper mapper;

        public MoviesController(IMovieService movieService, IMapper mapper)
        {
            this.movieService = movieService;
            this.mapper = mapper;
        }

        [HttpGet()]
        public IActionResult GetMovies()
        {
            var ownerId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            var moviesToReturn = mapper.Map<IEnumerable<Models.Movie>>(movieService.GetMoviesById(ownerId));

            return Ok(moviesToReturn);
        }

        [HttpGet("{id}", Name = "GetImage")]
        public IActionResult GetImage(Guid id)
        {
            var existingMovie = movieService.GetById(id);

            if (existingMovie == null)
            {
                return NotFound();
            }

            var movieToReturn = mapper.Map<Models.Movie>(existingMovie);

            return Ok(movieToReturn);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Subscribed")]
        public IActionResult AddReview(Guid id,
            [FromBody] MovieReview review)
        {
            var existingMovie = movieService.GetById(id);
            if (existingMovie == null)
            {
                return NotFound();
            }

            existingMovie.UpdateReview(review.Text);

            return NoContent();
        }
    }
}