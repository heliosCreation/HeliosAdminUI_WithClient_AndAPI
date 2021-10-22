using System.Collections.Generic;
using System.Net;

namespace API.Application.Response
{
    public class ApiResponse<T> where T:class
    {
        public ApiResponse()
        {
            Succeeded = true;
        }

        public ApiResponse(string message = null)
        {
            Succeeded = true;
            Message = message;
        }

        public ApiResponse(string message, bool success)
        {
            Succeeded = success;
            Message = message;
        }

        public bool Succeeded { get; set; } = true;

        public string Message { get; set; }

        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;

        public List<string> ErrorMessages { get; set; }

        public T Data { get; set; }

        public List<T> DataList { get; set; }
    }
}
