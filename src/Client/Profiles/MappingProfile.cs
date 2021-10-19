using AutoMapper;
using Movies.Client.Models.Movies;

namespace Movies.Client.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, UpdateMovieModel>();
        }
    }
}
