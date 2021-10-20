using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movies.Client.ApiService
{
    public interface IApiResponseParserService<T> where T: class
    {
        public Task<BaseResponse<T>> ParseResponse(HttpResponseMessage response, bool listExpected);
    }
}
