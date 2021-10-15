using MediatR;
using System.Collections.Generic;

namespace API.Application.Features.Categories.Query.GetAll
{
    public class GetAllCategoryQuery : IRequest<List<CategoryVm>>
    {
    }
}
