using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class CriticReview
    {
        int ID { get; set; }
        [Required]
        string Description { get; set; } = string.Empty;
        [Required]
        [Range(1,5)]
        int Rating { get; set; }
        Boolean IsPublished { get; set; }
        [Required]
        string CreatedBy { get; set; } = string.Empty;
        [Required]
        DateTime CreatedDate { get; set; }

        //TODO compare feature against other reviews/movies
    }
}
