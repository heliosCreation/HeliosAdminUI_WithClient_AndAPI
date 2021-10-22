using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Movies.Client.Models.ApplicationUserProfiles;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movies.Client.ApiService.ApplicationUserProfiles
{
    public class ApplicationUserProfileApiService : IApplicationUserProfileApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IApiResponseParserService<ApplicationUserProfile> _apiResponseParserService;
        private readonly HttpClient _client;


        public ApplicationUserProfileApiService(
            IHttpClientFactory httpClientFactory,
            IApiResponseParserService<ApplicationUserProfile> apiResponseParserService)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _apiResponseParserService = apiResponseParserService ?? throw new ArgumentNullException(nameof(apiResponseParserService));
            _client = _httpClientFactory.CreateClient("MovieAPIClient");

        }

        public async Task<BaseResponse<ApplicationUserProfile>> GetProfile(TicketReceivedContext receivedContext, string subject)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/ApplicationUserProfile/{subject}");
            request.SetBearerToken(receivedContext.Properties.GetTokenValue("access_token"));

            var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            return await _apiResponseParserService.ParseResponse(response, true, false);
        }
    }
}
