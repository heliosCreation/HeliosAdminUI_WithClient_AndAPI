using MediatR;

namespace API.Application.Features.ApplicationUserProfiles.Query.Get
{
    public class GetApplicationUserProfileQuery : IRequest<ApplicationUserProfileVm>
    {
        public GetApplicationUserProfileQuery(string sub)
        {
            Sub = sub;
        }

        public string Sub { get; set; }
    }
}
