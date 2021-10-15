using Microsoft.AspNetCore.Mvc.Rendering;
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
        [Display(Name ="Category")]
        public Guid CategoryId { get; set; }
        public List<Category> Categories { get; set; }

        public SelectList CategoryList
        {
            get
            {
                return Categories != null ?
                    new SelectList(Categories, nameof(Category.Id), nameof(Category.Name))
                    : null;
            }
        }

    }
}
