using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Client.Extensions.ServiceExtensions
{
    public static class AuthorizationService
    {
        public static IServiceCollection AddAuthorizationAndPolicies(this IServiceCollection services)
        {

            services.AddAuthorization(authorizationOptions =>
            {
                authorizationOptions.AddPolicy(
                    "CanOrderMovie",
                    policyBuilder =>
                    {
                        policyBuilder.RequireAuthenticatedUser();
                        policyBuilder.RequireClaim("country", "Fr");
                        policyBuilder.RequireClaim("subscriptionlevel", "payingUser");
                    });
            });
            return services;
        }
    }
}
