using API.Application.Contracts.Persistence;
using API.Application.Response;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Features.Categories.Query.GetAll
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, ApiResponse<CategoryVm>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoryQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }
        public async Task<ApiResponse<CategoryVm>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<CategoryVm>();
            var entities = await _categoryRepository.ListAllAsync();
            response.DataList =  _mapper.Map<List<CategoryVm>>(entities);
            return response;
        }
    }
}
