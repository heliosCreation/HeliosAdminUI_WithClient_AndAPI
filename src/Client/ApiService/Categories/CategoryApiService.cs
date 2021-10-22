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
        private readonly IApiResponseParserService<Category> _apiResponseParserService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpContext _httpContext;
        private readonly HttpClient _client;

        public CategoryApiService(
            IApiResponseParserService<Category> apiResponseParserService,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _apiResponseParserService = apiResponseParserService ?? throw new ArgumentNullException(nameof(apiResponseParserService));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpContext = httpContextAccessor.HttpContext;
            _client = _httpClientFactory.CreateClient("MovieAPIClient");
        }
        public async Task<BaseResponse<Category>> GetCategory()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,"/Category/{id}");
            var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            return await _apiResponseParserService.ParseResponse(response, true, false);
        }

        public async Task<BaseResponse<Category>> ListCategory()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,"/Category");
            var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            return await _apiResponseParserService.ParseResponse(response, true, true);
        }
    }
}
