using API.Domain.Common;
using System;

namespace API.Domain.Entities
{
    public class Movie : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid OwnerId { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
