using Microsoft.Extensions.DependencyInjection;
using Movies.Client.ApiService;
using Movies.Client.ApiService.ApplicationUserProfiles;
using Movies.Client.ApiService.Categories;
using Movies.Client.ApiService.Movies;

namespace Movies.Client.Extensions.ServiceExtensions
{
    public static class ApiServiceRegistration
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IApiResponseParserService<>), typeof(ApiResponseParserService<>));
            services.AddScoped<IApplicationUserProfileApiService, ApplicationUserProfileApiService>();
            services.AddScoped<IMovieApiService, MovieApiService>();
            services.AddScoped<ICategoryApiService, CategoryApiService>();

            return services;
        }
    }
}
