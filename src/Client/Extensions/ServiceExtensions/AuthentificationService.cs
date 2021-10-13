using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Movies.Client.PostConfigurationOptions;

namespace Movies.Client.Extensions.ServiceExtensions
{
    public static class AuthentificationService
    {
        public static IServiceCollection AddAuthentificationService(this IServiceCollection services)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //Make sure when someone connects, he goes through the OIDC
                opt.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
            {
                opt.AccessDeniedPath = "/Authorization/AccessDenied";
            })
            //Use those parameters for OIDC
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, opt =>
            {
                opt.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.Authority = "https://localhost:5005/";
                opt.ClientId = "movies_mvc_client";
                opt.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";

                opt.ResponseType = "code";
                opt.UsePkce = true;

                //Uncomment these to change the callback path set in the IDP
                //opt.CallbackPath = new PathString("");

                //opt.Scope.Add("address");
                opt.Scope.Add("email");
                //opt.Scope.Add("country");
                opt.Scope.Add("movieAPI");

                //Some claims are filtered by the middleware pipeline. With this command, we remove the filter.
                //opt.ClaimActions.Remove("nbf");
                opt.ClaimActions.MapAllExcept("sid", "idp", "s_hash", "auth_time");
                //opt.ClaimActions.MapUniqueJsonKey("country", "country");

                opt.SaveTokens = true;
                opt.GetClaimsFromUserInfoEndpoint = true;

                //Token should posesses those values
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role"
                };
            });

            services.AddSingleton<
                    IPostConfigureOptions<OpenIdConnectOptions>,
                    OpenIdConnectOptionsPostConfigureOptions>();

            return services;

        }
    }
}

