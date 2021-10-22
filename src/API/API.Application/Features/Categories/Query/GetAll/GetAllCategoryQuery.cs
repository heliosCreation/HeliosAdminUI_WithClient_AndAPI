using API.Application.Response;
using MediatR;

namespace API.Application.Features.Categories.Query.GetAll
{
    public class GetAllCategoryQuery : IRequest<ApiResponse<CategoryVm>>
    {
    }
}
