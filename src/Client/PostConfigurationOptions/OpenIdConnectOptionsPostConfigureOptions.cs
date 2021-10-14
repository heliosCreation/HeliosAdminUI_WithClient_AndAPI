using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Movies.Client.PostConfigurationOptions
{
    public class OpenIdConnectOptionsPostConfigureOptions : IPostConfigureOptions<OpenIdConnectOptions>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _options;


        public OpenIdConnectOptionsPostConfigureOptions(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        }
        public void PostConfigure(string name, OpenIdConnectOptions options)
        {
            options.Events = new OpenIdConnectEvents() 
            {
                OnTicketReceived = async ticketReceivedContext =>
                {
                    var subject = ticketReceivedContext.Principal.Claims
                        .FirstOrDefault(c => c.Type == "sub").Value;

                    var apiClient = _httpClientFactory.CreateClient("BasicMovieAPIClient");

                    var request = new HttpRequestMessage(
                        HttpMethod.Get,
                        $"/ApplicationUserProfile/{subject}");

                    request.SetBearerToken(
                        ticketReceivedContext.Properties.GetTokenValue("access_token"));

                    var response = await apiClient.SendAsync(
                        request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                    response.EnsureSuccessStatusCode();

                    var applicationUserProfile = new ApplicationUserProfileModel();
                    var stream = await response.Content.ReadAsStreamAsync();
                    var jsonResult = await JsonSerializer.DeserializeAsync<ApplicationUserProfileModel>(stream, _options);

                    var newClaimsIdentity = new ClaimsIdentity();
                    var claims = new List<Claim>()
                    {
                        new Claim("subscriptionlevel", jsonResult.SubscriptionLevel),
                        new Claim(ClaimTypes.Role, jsonResult.Role)
                    };
                    newClaimsIdentity.AddClaims(claims);

                    // add this additional identity
                    ticketReceivedContext.Principal.AddIdentity(newClaimsIdentity);
                }
            };
        }
    }
}
