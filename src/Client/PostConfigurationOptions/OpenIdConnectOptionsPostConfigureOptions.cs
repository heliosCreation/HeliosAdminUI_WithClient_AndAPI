using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using Movies.Client.ApiService.ApplicationUserProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Movies.Client.PostConfigurationOptions
{
    public class OpenIdConnectOptionsPostConfigureOptions : IPostConfigureOptions<OpenIdConnectOptions>
    {
        private readonly IApplicationUserProfileApiService _applicationUserProfileApiService;

        public OpenIdConnectOptionsPostConfigureOptions(IApplicationUserProfileApiService applicationUserProfileApiService)
        {
            _applicationUserProfileApiService = applicationUserProfileApiService ?? throw new ArgumentNullException(nameof(applicationUserProfileApiService));
        }

        public void PostConfigure(string name, OpenIdConnectOptions options)
        {
            options.Events = new OpenIdConnectEvents()
            {
                OnTicketReceived = async ticketReceivedContext =>
                {
                    var subject = ticketReceivedContext.Principal.Claims
                        .FirstOrDefault(c => c.Type == "sub").Value;

                    var jsonResult = await _applicationUserProfileApiService.GetProfile(ticketReceivedContext, subject);

                    var newClaimsIdentity = new ClaimsIdentity();
                    var claims = new List<Claim>()
                    {
                        new Claim("subscriptionlevel", jsonResult.Data.SubscriptionLevel),
                        new Claim(ClaimTypes.Role, jsonResult.Data.Role)
                    };
                    newClaimsIdentity.AddClaims(claims);

                    // add this additional identity
                    ticketReceivedContext.Principal.AddIdentity(newClaimsIdentity);
                }
            };
        }
    }
}
