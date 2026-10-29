using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class CriticReview
    {
        //KEYS
        public int ID { get; set; }//PK

        public int MovieID { get; set; }//FK

        //nav property
        [ValidateNever]
        public Movie Movie { get; set; } = null!;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0, 5)]
        public float Rating { get; set; }


        public bool IsPublished { get; set; }

        [Required]
        public string CreatedBy { get; set; } = string.Empty;

        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        //TODO compare feature against other reviews/movies
    }
}
