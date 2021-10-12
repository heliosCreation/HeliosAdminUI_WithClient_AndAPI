using API.Application.Contracts.Persistence;
using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace API.Persistence.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(MovieAPIDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<bool> CategoryExist(Guid id)
        {
            return await _dbContext.Categories.AnyAsync(c => c.Id == id);
        }
    }
}
