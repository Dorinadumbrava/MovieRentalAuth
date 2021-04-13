using MovieRental.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRental.API.Services
{
    public class MovieService : IMovieService
    {
        private List<Movie> movies = new List<Movie>
            {
            new Movie{ Id = new Guid("8554138e-0c2f-4a81-8092-146729412468"), FileName = "Amelie", OwnerId= "d860efca-22d9-47fd-8249-791ba61b07c7", Title = "Amelie"},
            new Movie{ Id = new Guid("55a2dfe1-8692-484b-9eab-2b4c9077c203"), FileName = "MonaLisa", OwnerId= "b7539694-97e7-4dfe-84da-b4256e1ff5c7", Title = "MonaLisa Smile"},
            new Movie{ Id = new Guid("ffc1c0d2-0724-44c0-aeec-d5671bd7702f"), FileName = "Butterfly", OwnerId= "b7539694-97e7-4dfe-84da-b4256e1ff5c7", Title = "Butterfly effect"},
            new Movie{ Id = new Guid("a6374ba5-da1e-40f9-b015-734982f4898c"), FileName = "Shrek", OwnerId= "d860efca-22d9-47fd-8249-791ba61b07c7", Title = "Shrek"}
            };

        public Movie GetById(Guid id)
        {
            return movies.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Movie> GetMoviesById(string id)
        {
            return movies.Where(x => x.OwnerId == id).OrderBy(x => x.Title);
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return movies.OrderBy(x => x.Title);
        }
    }
}
