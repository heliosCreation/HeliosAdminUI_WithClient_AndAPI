using API.Application.Response;
using MediatR;
using System;

namespace API.Application.Features.Movies.Command.Delete
{
    public class DeleteMovieCommand : IRequest<ApiResponse<object>>
    {
        public DeleteMovieCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
