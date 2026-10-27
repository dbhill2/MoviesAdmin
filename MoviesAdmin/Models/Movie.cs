using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Movie
    {
        //KEYS
        public int ID { get; set; }//Primary Key

        public int GenreID { get; set; }//FK

        public int AudienceRatingID { get; set; }//FK

        public int CriticReviewID { get; set; }//FK

        //Nav Properties
        [ValidateNever]
        public Genre Genre { get; set; } = null!;

        [ValidateNever]
        public AudienceRating AudienceRating { get; set; } = null!;

        [ValidateNever]
        public CriticReview CriticReview { get; set; } = null!;

        //Movie Fields
        [StringLength(50, MinimumLength = 1)]
        [Required]
        [Display(Prompt = "Movie Title")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, MinimumLength = 1)]
        [Required]
        [Display(Prompt = "Write a short description of the movie...")]
        public string Synopsis { get; set; } = string.Empty;


        [Range(1, 51420)] //Longest movie ever made
        [Required]
        [Display(Name = "Run Time", Prompt = "Between 1 - 51420 minutes")]
        public int RunTime { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateOnly ReleaseDate { get; set; }

        [Display(Name = "Poster")]
        public string ImageUrl { get; set; } = string.Empty;

        [Display(Name = "Now Playing")]
        public bool NowPlaying { get; set; } //Added field to show nearby theatres playing this movie
    }
}