using API.Application.Response;
using MediatR;

namespace API.Application.Features.ApplicationUserProfiles.Query.Get
{
    public class GetApplicationUserProfileQuery : IRequest<ApiResponse<ApplicationUserProfileVm>>
    {
        public GetApplicationUserProfileQuery(string sub)
        {
            Sub = sub;
        }

        public string Sub { get; set; }
    }
}
