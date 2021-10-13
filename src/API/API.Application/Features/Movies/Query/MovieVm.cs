using System;

namespace API.Application.Features.Movies.Query
{
    public class MovieVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid OwnerId { get; set; }
        public CategoryDto Category { get; set; }
    }
}
