using API.Application.Contracts.Identity;
using API.Domain.Entities;
using API.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Identity.Services
{
    public class ApplicationUserProfileRepository : IApplicatonUserProfileRepository
    {
        private readonly MovieAPIDbContext _context;

        public ApplicationUserProfileRepository(MovieAPIDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ApplicationUserProfile>> GetAllApplicationUserProfile()
        {
            return await _context.UserProfiles.ToListAsync();
        }
        public async Task<ApplicationUserProfile> GetApplicationUserProfile(string subject)
        {
            return await _context.UserProfiles.FirstOrDefaultAsync(a => a.Subject == subject);
        }

        public async Task<bool> ApplicationUserProfileExists(string subject)
        {
            return await _context.UserProfiles.AnyAsync(a => a.Subject == subject);
        }

        public async Task AddApplicationUserProfile(ApplicationUserProfile applicationUserProfile)
        {
            await _context.UserProfiles.AddAsync(applicationUserProfile);
            await _context.SaveChangesAsync();
        }
    }
}
