using API.Application.Response;
using MediatR;
using System.Collections.Generic;

namespace API.Application.Features.Movies.Query.GetAll
{
    public class GetMovieListQuery : IRequest<ApiResponse<MovieVm>>
    {
    }
}
