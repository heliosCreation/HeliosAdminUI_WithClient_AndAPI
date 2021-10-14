using API.Application.Contracts.Identity;
using API.Domain.Entities;
using API.Persistence;
using API.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace API.Identity.Repository
{
    public class ApplicationUserProfileRepository : BaseRepository<ApplicationUserProfile>, IApplicatonUserProfileRepository
    {
        public ApplicationUserProfileRepository(MovieAPIDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> ApplicationUserProfileExists(string subject)
        {
            return await _dbContext.UserProfiles.AnyAsync(a => a.Subject == subject);
        }

        public async Task<ApplicationUserProfile> GetApplicationUserProfileBySubject(string subject)
        {
            return await _dbContext.UserProfiles.Where(p => p.Subject == subject).FirstOrDefaultAsync();
        }
    }
}
