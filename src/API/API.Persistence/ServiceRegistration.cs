using API.Application.Contracts.Persistence;
using API.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MovieAPIDbContext>(
                opt => opt.UseSqlServer(configuration.GetConnectionString("MovieApiData"),
                b => b.MigrationsAssembly(typeof(MovieAPIDbContext).Assembly.FullName))
            );

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IApplicationUserProfileRepository, ApplicationUserProfileRepository>();

            return services;
        }
    }
}
