using API.Application.Contracts.Persistence;
using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Persistence.Repository
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieAPIDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Movie> GetByIdAndOwnerId(Guid id, Guid userId)
        {
            return await _dbContext.Movies
                .Where(m => m.Id == id && m.OwnerId == userId)
                .Include(m => m.Category)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Movie>> GetByOwnerId(Guid userId)
        {
            return await _dbContext.Movies
                .Where(m => m.OwnerId == userId)
                .Include(m => m.Category)
                .ToListAsync();
        }

        public async Task<bool> IsMovieNameUniqueForUserAndCategory(string entityName, Guid uid, Guid categoryId)
        {
            return await _dbContext.Movies.AnyAsync(m =>
            m.Name == entityName &&
            m.OwnerId == uid &&
            m.CategoryId == categoryId);
        }
    }
}
