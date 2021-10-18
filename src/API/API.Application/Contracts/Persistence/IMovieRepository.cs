using API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Application.Contracts.Persistence
{
    public interface IMovieRepository : IAsyncRepository<Movie>
    {
        Task<bool> IsMovieNameUniqueForUserAndCategory(string entityName, Guid uid, Guid categoryId);

        Task<Movie> GetByIdAndOwnerId(Guid id, Guid userId);

        Task<List<Movie>> GetByOwnerId(Guid userId);
        Task<List<Movie>> GetAllTest();
        Task<bool> IsMovieNameUniqueForUserAndCategoryOnUpdate(string entityName, Guid id, Guid uid, Guid categoryId);
    }
}
