using API.Application.Features.Movies.Command.Create;
using API.Domain.Entities;
using AutoMapper;

namespace API.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateMovieCommand, Movie>();
            CreateMap<Movie, CreateMovieDto>();
        }
    }
}
