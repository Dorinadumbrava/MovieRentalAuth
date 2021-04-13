using System;
using System.Collections.Generic;
using MovieRental.API.Entities;

namespace MovieRental.API.Services
{
    public interface IMovieService
    {
        Movie GetById(Guid id);
        IEnumerable<Movie> GetMoviesById(string id);
        IEnumerable<Movie> GetAllMovies();
    }
}