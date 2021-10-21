using System.Collections.Generic;
using System.Net;

namespace API.Application.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = true;
        }

        public BaseResponse(string message = null)
        {
            Success = true;
            Message = message;
        }

        public BaseResponse(string message, bool success)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }

        public string Message { get; set; }

        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;

        public List<string> ErrorMessages { get; set; }
    }
}
