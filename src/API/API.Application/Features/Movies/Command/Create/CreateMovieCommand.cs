using API.Application.Response;
using MediatR;
using System;

namespace API.Application.Features.Movies.Command.Create
{
    public class CreateMovieCommand : IRequest<ApiResponse<CreateMovieDto>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid OwnerId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
