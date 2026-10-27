using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class CriticReview
    {
        public int ID { get; set; }

        //nav property
        [ValidateNever]
        public List<Movie> Movie { get; set; } = new List<Movie>();


        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0,5)]
        public float Rating { get; set; }

        public bool IsPublished { get; set; }
        [Required]
        public string CreatedBy { get; set; } = string.Empty;
        [Required]
        public DateTime CreatedDate { get; set; }

        //TODO compare feature against other reviews/movies
    }
}
