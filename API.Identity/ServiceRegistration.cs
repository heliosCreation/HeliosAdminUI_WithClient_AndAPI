using API.Application.Contracts.Identity;
using API.Identity.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Identity
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.Authority = configuration["JwtSettings:Authority"];
                opt.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
            });


            services.AddScoped<IApplicatonUserProfileRepository, ApplicationUserProfileRepository>();
            return services;
        }
    }
}
