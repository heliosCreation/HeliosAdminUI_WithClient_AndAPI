using API.Domain.Common;
using System;

namespace API.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
