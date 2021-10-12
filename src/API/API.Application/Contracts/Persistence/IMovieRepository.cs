using API.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace API.Application.Contracts.Persistence
{
    public interface IMovieRepository : IAsyncRepository<Movie>
    {
        Task<bool> IsMovieNameUniqueForUserAndCategory(string entityName, Guid uid, Guid categoryId);
    }
}
