using System;

namespace API.Application.Features.Movies.Command.Create
{
    public class CreateMovieDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}