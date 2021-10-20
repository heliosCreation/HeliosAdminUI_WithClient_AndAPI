using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Movies.Client.ApiService
{
    public class ApiResponseParserService<T> : IApiResponseParserService<T> where T : class
    {
        private readonly JsonSerializerOptions _options;

        public ApiResponseParserService()
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        }
        public async Task<BaseResponse<T>> ParseResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var result = new BaseResponse<T> { Succeeded = false };

                if (response.StatusCode == HttpStatusCode.Unauthorized ||
                    response.StatusCode == HttpStatusCode.Forbidden)
                {
                    result.StatusCode = 401;
          
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    result.StatusCode = 404;
                }
                return result;
            }


            var stream = await response.Content.ReadAsStreamAsync();
            var data = await JsonSerializer.DeserializeAsync<T>(stream, _options);
            return new BaseResponse<T>
            {
                Succeeded = true,
                StatusCode = 200,
                Data = data
            };
        }
    }
}
