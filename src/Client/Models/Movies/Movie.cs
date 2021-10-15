using Movies.Client.Models.Categories;
using System;

namespace Movies.Client.Models.Movies
{
    public class Movie
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Category Category { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

    }
}
