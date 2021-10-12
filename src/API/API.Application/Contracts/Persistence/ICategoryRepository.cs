using API.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace API.Application.Contracts.Persistence
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
        Task<bool> CategoryExist(Guid id);
    }
}
