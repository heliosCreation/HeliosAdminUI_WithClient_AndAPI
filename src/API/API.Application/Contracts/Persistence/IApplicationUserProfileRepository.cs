using API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Application.Contracts.Persistence
{
    public interface IApplicationUserProfileRepository : IAsyncRepository<ApplicationUserProfile>
    {
    }
}
