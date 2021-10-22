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

        /// <summary>
        /// Parse the status code of an HttpResponse message and generate the BaseResponse use by the client accordingly.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="expectData"></param>
        /// <param name="listExpected"></param>
        /// <returns></returns>
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
                    case HttpStatusCode.BadRequest:
                        result.StatusCode = 400;
                        var errorsFromStream = await response.Content.ReadAsStreamAsync();
                        var errors =  await JsonSerializer.DeserializeAsync<BaseResponse<T>>(errorsFromStream, _options);
                        result.ErrorMessages = errors.ErrorMessages;
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

        /// <summary>
        /// Determine if data is expected from the api call( Aka, T, List<T>).
        /// If not, return the a valid BaseResponse.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="expectData"></param>
        /// <param name="listExpected"></param>
        /// <param name="response"></param>
        /// <returns> A BaseResponse object.</returns>
        private async Task<BaseResponse<T>> CreateListOrSingleObjectResponse(Stream stream, bool expectData, bool listExpected, BaseResponse<T> response)
        {
            if (!expectData)
            {
                return response;
            }
            var deserialized = await JsonSerializer.DeserializeAsync<BaseResponse<T>>(stream, _options);

            if (!listExpected)
            {
                response.Data = deserialized.Data;
                return response;
            }
            response.DataList = deserialized.DataList;
            return response;
        }

    }
}
