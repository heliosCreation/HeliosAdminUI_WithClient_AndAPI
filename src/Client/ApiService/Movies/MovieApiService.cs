using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Movies.Client.ApiService.Movies;
using Movies.Client.Models;
using Movies.Client.Models.Movies;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Movies.Client.ApiService
{
    public class MovieApiService : IMovieApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IApiResponseParserService<Movie> _apiResponseParserService;
        private readonly HttpContext _httpContext;
        private readonly HttpClient _client;

        public MovieApiService(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IApiResponseParserService<Movie> apiResponseParserService)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _apiResponseParserService = apiResponseParserService ?? throw new ArgumentNullException(nameof(apiResponseParserService));
            _httpContext = httpContextAccessor.HttpContext;
            _client = _httpClientFactory.CreateClient("MovieAPIClient");
        }


        public async Task<UserInfoViewModel> GetUserInfo()
        {
            var idpClient = _httpClientFactory.CreateClient("IDPClient");

            var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();

            if (metaDataResponse.IsError)
            {
                throw new HttpRequestException("Something went wrong when trying to get the discovery endpoint");
            }

            var accessToken = await _httpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            var userInfo = await idpClient.GetUserInfoAsync(
                new UserInfoRequest
                {
                    Address = metaDataResponse.UserInfoEndpoint,
                    Token = accessToken
                });

            var userInfoDictionnary = new Dictionary<string, string>();
            foreach (var claim in userInfo.Claims)
            {
                if (userInfoDictionnary.ContainsKey(claim.Type))
                {
                    userInfoDictionnary[claim.Type] = $"{userInfoDictionnary[claim.Type]} , {claim.Value}";
                }
                else
                {
                    userInfoDictionnary.Add(claim.Type, claim.Value);
                }

            }

            return new UserInfoViewModel(userInfoDictionnary);
        }



        public async Task<BaseResponse<Movie>> GetMovies()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/movie");
            var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            return await _apiResponseParserService.ParseResponse(response, true, true);
        }

        public async Task<BaseResponse<Movie>> GetMovie(Guid? id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/movie/{id}");
            var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            return await _apiResponseParserService.ParseResponse(response, true, false);

        }

        public async Task<BaseResponse<Movie>> CreateMovie(CreateMovieModel movie)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/movie")
            {
                Content = JsonContent.Create(movie)
            };
            var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            return await _apiResponseParserService.ParseResponse(response, true, false);
        }

        public async Task<BaseResponse<Movie>> UpdateMovie(UpdateMovieModel movie)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"/movie/{movie.Id}")
            {
                Content = JsonContent.Create(movie)
            };
            var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            return await _apiResponseParserService.ParseResponse(response, false, false);

        }

        public async Task<BaseResponse<Movie>> DeleteMovie(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"movie/{id}");
            var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            return await _apiResponseParserService.ParseResponse(response, false, false);
        }

    }
}
