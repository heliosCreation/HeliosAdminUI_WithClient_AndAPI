using API.Application.Contracts.Persistence;
using API.Domain.Entities;
using System.Threading.Tasks;

namespace API.Application.Contracts.Identity
{
    public interface IApplicatonUserProfileRepository : IAsyncRepository<ApplicationUserProfile>
    {
        Task<bool> ApplicationUserProfileExists(string subject);

        Task<ApplicationUserProfile> GetApplicationUserProfileBySubject(string subject);
    }
}
