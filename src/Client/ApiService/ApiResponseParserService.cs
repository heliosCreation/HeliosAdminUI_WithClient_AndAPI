using System.Collections.Generic;
using System.IO;
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
        public async Task<BaseResponse<T>> ParseResponse(HttpResponseMessage response, bool listExpected)
        {
            var result = new BaseResponse<T>();
            if (!response.IsSuccessStatusCode)
            {
                result.Succeeded = false;
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

            result.Succeeded = true;
            var stream = await response.Content.ReadAsStreamAsync();
            return await CreateListOrSingleObjectResponse(stream, listExpected, result);

        }


        private async Task<BaseResponse<T>> CreateListOrSingleObjectResponse(Stream stream, bool listExpected, BaseResponse<T> response)
        {
            if (!listExpected)
            {
                var data = await JsonSerializer.DeserializeAsync<T>(stream, _options);
                response.Data = data;
                return response;
            }
            var dataList = await JsonSerializer.DeserializeAsync<IEnumerable<T>>(stream, _options);
            response.DataList = dataList;
            return response;
        }

    }
}
