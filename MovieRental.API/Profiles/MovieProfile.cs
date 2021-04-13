using AutoMapper;

namespace MovieRental.API.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Entities.Movie, Models.Movie>().ReverseMap();
        }
    }
}