using API.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Application.Contracts.Identity
{
    public interface IApplicatonUserProfileRepository
    {
        Task<bool> ApplicationUserProfileExists(string subject);
    }
}
