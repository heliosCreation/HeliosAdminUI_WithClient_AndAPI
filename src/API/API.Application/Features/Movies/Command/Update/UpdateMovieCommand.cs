using API.Application.Response;
using MediatR;
using System;

namespace API.Application.Features.Movies.Command.Update
{
    public class UpdateMovieCommand : IRequest<ApiResponse<object>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}
