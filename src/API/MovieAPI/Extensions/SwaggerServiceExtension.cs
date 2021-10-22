using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MovieAPI.Extensions
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Movie - WebApi",
                        Description = "This Api will be responsible for managing the movies and completing claim bases.",
                        Contact = new OpenApiContact
                        {
                            Name = "HeliosCreation",
                            Email = "reliableDevelopment@hotmail.com",
                        }
                    });
            });
            return services;
        }
    }
}
