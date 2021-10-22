using API.Application.Contracts.Persistence;
using API.Application.Exceptions;
using API.Application.Response;
using API.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Features.Categories.Query.Get
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, ApiResponse<CategoryVm>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<ApiResponse<CategoryVm>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<CategoryVm>();
            var entity = await _categoryRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                response.Succeeded = false;
                response.StatusCode = (int)HttpStatusCode.NotFound;
                return response;
            }

            response.Data = _mapper.Map<CategoryVm>(entity);
            return response;

        }
    }
}
