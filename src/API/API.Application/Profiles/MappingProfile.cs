using API.Application.Features.Movies.Command.Create;
using API.Application.Features.Movies.Query;
using API.Application.Features.Movies.Query.Get;
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
            CreateMap<Movie, MovieVm>();
        }
    }
}
