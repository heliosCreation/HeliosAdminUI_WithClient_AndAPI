using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Movies.Client.Models.ApplicationUserProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movies.Client.ApiService.ApplicationUserProfiles
{
    public class ApplicationUserProfileApiService : IApplicationUserProfileApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IApiResponseParserService<ApplicationUserProfile> _apiResponseParserService;

        public ApplicationUserProfileApiService(
            IHttpClientFactory httpClientFactory,
            IApiResponseParserService<ApplicationUserProfile> apiResponseParserService)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _apiResponseParserService = apiResponseParserService ?? throw new ArgumentNullException(nameof(apiResponseParserService));
        }

        public async Task<BaseResponse<ApplicationUserProfile>> GetProfile(TicketReceivedContext receivedContext, string subject)
        {
            var client = _httpClientFactory.CreateClient("BasicMovieAPIClient");

            var request = new HttpRequestMessage(HttpMethod.Get,$"/ApplicationUserProfile/{subject}");
            request.SetBearerToken(receivedContext.Properties.GetTokenValue("access_token"));

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            return await _apiResponseParserService.ParseResponse(response, true, false);
        }
    }
}
