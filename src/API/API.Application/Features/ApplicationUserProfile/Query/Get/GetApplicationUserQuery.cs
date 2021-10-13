using MediatR;

namespace API.Application.Features.ApplicationUserProfile.Query.Get
{
    public class GetApplicationUserQuery : IRequest<ApplicationUserProfileVm>
    {
        public GetApplicationUserQuery(string sub)
        {
            Sub = sub;
        }

        public string Sub { get; set; }
    }
}
