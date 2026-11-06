using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class CriticReview
    {
        //KEYS
        public int ID { get; set; }//PK

        [Range(1, int.MaxValue, ErrorMessage = "Please select a movie")]
        public int MovieID { get; set; }//FK

        //nav property
        [ValidateNever]
        [ScaffoldColumn(false)]        
        public Movie Movie { get; set; } = null!;

        [StringLength(1000, MinimumLength = 1)]
        [Required]
        [Display(Name = "Review", Prompt = "1-1000 characters")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0, 5)]
        [Display(Name = "Star Rating", Prompt = "0 - 5")]
        public float Rating { get; set; }

        [Display(Prompt = "Published")]
        public bool IsPublished { get; set; }

        [ValidateNever]
        [Display(Prompt ="Created By")]
        public string CreatedBy { get; set; } = string.Empty;

        [Display(Prompt = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
