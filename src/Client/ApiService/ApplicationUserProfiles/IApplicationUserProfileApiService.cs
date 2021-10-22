using Microsoft.AspNetCore.Authentication;
using Movies.Client.Models.ApplicationUserProfiles;
using System.Threading.Tasks;

namespace Movies.Client.ApiService.ApplicationUserProfiles
{
    public interface IApplicationUserProfileApiService
    {
        Task<BaseResponse<ApplicationUserProfile>> GetProfile(TicketReceivedContext receivedContext, string subject);
    }
}
