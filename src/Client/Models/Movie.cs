using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.Client.Models
{
    public class Movie
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

    }
}
