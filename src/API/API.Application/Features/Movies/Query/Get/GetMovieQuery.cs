using API.Application.Response;
using MediatR;
using System;

namespace API.Application.Features.Movies.Query.Get
{
    public class GetMovieQuery : IRequest<ApiResponse<MovieVm>>
    {
        public GetMovieQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
