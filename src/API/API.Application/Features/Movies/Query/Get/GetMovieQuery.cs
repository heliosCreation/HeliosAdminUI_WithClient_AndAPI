using MediatR;
using System;

namespace API.Application.Features.Movies.Query.Get
{
    public class GetMovieQuery : IRequest<MovieVm>
    {
        public GetMovieQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
