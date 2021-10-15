using API.Application.Features.ApplicationUserProfiles.Query;
using API.Application.Features.Categories.Query;
using API.Application.Features.Movies.Command.Create;
using API.Application.Features.Movies.Query;
using API.Domain.Entities;
using AutoMapper;

namespace API.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryVm>();

            CreateMap<CreateMovieCommand, Movie>();
            CreateMap<Movie, CreateMovieDto>();
            CreateMap<Movie, MovieVm>();


            CreateMap<ApplicationUserProfile, ApplicationUserProfileVm>();

        }
    }
}
