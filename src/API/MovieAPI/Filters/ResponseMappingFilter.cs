using API.Application.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace MovieAPI.Filters
{
    public class ResponseMappingFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult && objectResult.Value is BaseResponse baseResponse && baseResponse.StatusCode != (int)HttpStatusCode.OK)
                context.Result = new ObjectResult(new { baseResponse.ErrorMessages }) { StatusCode = (int)baseResponse.StatusCode };
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
