using MediatR;
using System;

namespace API.Application.Features.Movies.Command.Create
{
    public class CreateMovieCommand : IRequest<CreateMovieCommandResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid OwnerId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
