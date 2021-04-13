
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
            var imagesToReturn = mapper.Map<IEnumerable<Models.Movie>>(movieService.GetAllMovies());

            return Ok(imagesToReturn);
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