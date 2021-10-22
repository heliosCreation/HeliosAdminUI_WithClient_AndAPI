using Movies.Client.Models.Categories;
using System.Threading.Tasks;

namespace Movies.Client.ApiService.Categories
{
    public interface ICategoryApiService
    {
        Task<BaseResponse<Category>> GetCategory();
        Task<BaseResponse<Category>> ListCategory();
    }
}
