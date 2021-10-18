using API.Application.Contracts.Identity;
using API.Identity.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Identity
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer(opt =>
                {
                    opt.Authority = "https://localhost:5005";
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(option =>
            {
                option.AddPolicy("ClientIdPolicy", policy => policy.RequireClaim("client_id", "movie_api_client", "movies_mvc_client"));
            });

            services.AddScoped<IApplicatonUserProfileRepository, ApplicationUserProfileRepository>();
            return services;
        }
    }
}
