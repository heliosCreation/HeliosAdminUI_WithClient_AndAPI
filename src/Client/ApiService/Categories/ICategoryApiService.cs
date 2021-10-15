using Movies.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Client.ApiService.Categories
{
    public interface ICategoryApiService
    {
        Task<Category> GetCategory();
        Task<List<Category>> ListCategory();
    }
}
