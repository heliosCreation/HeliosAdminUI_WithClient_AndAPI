using API.Application.Response;
using MediatR;
using System;

namespace API.Application.Features.Categories.Query.Get
{
    public class GetCategoryQuery : IRequest<ApiResponse<CategoryVm>>
    {
        public GetCategoryQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
