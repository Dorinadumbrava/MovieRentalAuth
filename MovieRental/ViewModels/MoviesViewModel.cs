using MovieRental.Models;
using System.Collections.Generic;

namespace MovieRental.Client.ViewModels
{
    public class MoviesViewModel
    {
        public IEnumerable<Movie> Movies { get; private set; }
            = new List<Movie>();

        public MoviesViewModel(IEnumerable<Movie> movies)
        {
            Movies = movies;
        }
    }
}