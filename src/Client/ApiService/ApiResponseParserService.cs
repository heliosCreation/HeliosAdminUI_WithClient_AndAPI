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
        public async Task<BaseResponse<T>> ParseResponse(HttpResponseMessage response,bool expectData, bool listExpected)
        {
            var result = new BaseResponse<T>();
            result.Succeeded = response.IsSuccessStatusCode;

            if (!result.Succeeded)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                    case HttpStatusCode.Forbidden:
                        result.StatusCode = 401;
                        break;
                    case HttpStatusCode.NotFound:
                        result.StatusCode = 404;
                        break;
                    default:
                        result.StatusCode = 500;
                        break;
                }
                return result;
            }


            var stream = await response.Content.ReadAsStreamAsync();
            return await CreateListOrSingleObjectResponse(stream,expectData, listExpected, result);

        }


        private async Task<BaseResponse<T>> CreateListOrSingleObjectResponse(Stream stream, bool expectData, bool listExpected, BaseResponse<T> response)
        {
            if (!expectData)
            {
                return response;
            }
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
