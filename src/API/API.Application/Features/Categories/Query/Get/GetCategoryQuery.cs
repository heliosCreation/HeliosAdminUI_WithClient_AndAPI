using MediatR;
using System;

namespace API.Application.Features.Categories.Query.Get
{
    public class GetCategoryQuery : IRequest<CategoryVm>
    {
        public GetCategoryQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
