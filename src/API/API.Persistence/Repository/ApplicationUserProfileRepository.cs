using API.Application.Contracts.Persistence;
using API.Domain.Entities;

namespace API.Persistence.Repository
{
    public class ApplicationUserProfileRepository : BaseRepository<ApplicationUserProfile>, IApplicationUserProfileRepository
    {
        public ApplicationUserProfileRepository(MovieAPIDbContext dbContext) : base(dbContext)
        {

        }
    }
}
