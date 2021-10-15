using Microsoft.AspNetCore.Http;
using Movies.Client.Models.Categories;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Movies.Client.ApiService.Categories
{
    public class CategoryApiService : ICategoryApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpContext _httpContext;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public CategoryApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpContext = httpContextAccessor.HttpContext;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _client = _httpClientFactory.CreateClient("MovieAPIClient");
        }
        public async Task<Category> GetCategory()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "/Category/{id}");

            var response = await _client.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();
            var category = await JsonSerializer.DeserializeAsync<Category>(stream, _options);
            return category;
        }

        public async Task<List<Category>> ListCategory()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "/Category");

            var response = await _client.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();
            var categories = await JsonSerializer.DeserializeAsync<List<Category>>(stream, _options);
            return categories;
        }
    }
}
