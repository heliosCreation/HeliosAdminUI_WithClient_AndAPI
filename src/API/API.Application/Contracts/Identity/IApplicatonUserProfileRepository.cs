using API.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Application.Contracts.Identity
{
    public interface IApplicatonUserProfileRepository
    {
        Task AddApplicationUserProfile(ApplicationUserProfile applicationUserProfile);
        Task<bool> ApplicationUserProfileExists(string subject);
        Task<IReadOnlyList<ApplicationUserProfile>> GetAllApplicationUserProfile();
        Task<ApplicationUserProfile> GetApplicationUserProfile(string subject);
    }
}
