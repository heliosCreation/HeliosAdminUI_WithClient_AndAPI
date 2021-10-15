using Movies.Client.Models.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.Client.Models.Movies
{
    public class CreateMovieModel
    {
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; } 
        
        [Required]
        public Category SelectedCategory { get; set; }
        public List<Category> Categories { get; set; }

    }
}
