using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MovieAPIDbContext>(
                opt => opt.UseSqlServer(configuration.GetConnectionString("MovieAPIDataConnectionString"),
                b => b.MigrationsAssembly(typeof(MovieAPIDbContext).Assembly.FullName))
            );
            return services;

        }
    }
}
