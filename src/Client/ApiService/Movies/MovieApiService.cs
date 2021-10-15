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
using System.Text.Json;
using System.Threading.Tasks;

namespace Movies.Client.ApiService
{
    public class MovieApiService : IMovieApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpContext _httpContext;
        private readonly JsonSerializerOptions _options;

        public MovieApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpContext = httpContextAccessor.HttpContext;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
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



        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var client = _httpClientFactory.CreateClient("MovieAPIClient");

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "/movie");

            var response = await client.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();
            var movies = await JsonSerializer.DeserializeAsync<List<Movie>>(stream, _options);
            return movies;
        }

        public async Task<Movie> GetMovie(Guid? id)
        {
            var client = _httpClientFactory.CreateClient("MovieAPIClient");

            var request = new HttpRequestMessage(HttpMethod.Get, $"/movie/{id}");

            var response = await client.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var movie = await JsonSerializer.DeserializeAsync<Movie>(stream, _options);
            return movie;
        }

        public async Task CreateMovie(Movie movie)
        {
            var client = _httpClientFactory.CreateClient("MovieAPIClient");

            var request = new HttpRequestMessage(HttpMethod.Post, "/movie")
            {
                Content = JsonContent.Create(movie)
            };

            var response = await client.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateMovie(Movie movie)
        {
            var client = _httpClientFactory.CreateClient("MovieAPIClient");

            var request = new HttpRequestMessage(HttpMethod.Put, $"/movie/{movie.Id}")
            {
                Content = JsonContent.Create(movie)
            };

            var response = await client.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteMovie(Guid id)
        {
            var client = _httpClientFactory.CreateClient("MovieAPIClient");

            var request = new HttpRequestMessage(HttpMethod.Delete, $"movie/{id}");

            var response = await client.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }

    }
}
