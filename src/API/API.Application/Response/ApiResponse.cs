using System.Collections.Generic;
using System.Net;

namespace API.Application.Response
{
    public class ApiResponse<T> : BaseResponse where T:class 
    {

        public T Data { get; set; }

        public List<T> DataList { get; set; }
    }
}
