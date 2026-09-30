using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [Required]
        [Display(Prompt = "Movie Title")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, MinimumLength = 1)]
        [Required]
        [Display(Prompt = "Write a short description of the movie...")]
        public string Synopsis { get; set; } = string.Empty;

        [StringLength(50, MinimumLength = 1)]
        [Required]
        [Display(Prompt = "Ex: Horror, Action, Adventure,...")]
        public string Genre { get; set; } = string.Empty;

        [StringLength(5, MinimumLength = 1)]
        [Required]
        [Display(Prompt = "Ex: G,PG,PG-13,...")]
        public string Rating { get; set; } = string.Empty;

        [Range(1, 51420)] //Longest movie ever made
        [Required]
        [Display(Name = "Run Time", Prompt = "Between 1 - 51420 minutes")]
        public int RunTime { get; set; }

        [Display(Name = "Release Date")]
        public DateOnly ReleaseDate { get; set; }

        [Display(Name = "Now Playing")]
        public Boolean NowPlaying { get; set; } //Added field to show nearby theatres playing this movie
    }
}