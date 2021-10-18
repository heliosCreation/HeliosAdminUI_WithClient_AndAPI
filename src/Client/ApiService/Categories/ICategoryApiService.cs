using Movies.Client.Models;
using Movies.Client.Models.Categories;
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
