using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [Required(ErrorMessage = "Title must be between 1-50")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, MinimumLength = 1)]
        [Required]
        public string Synopsis { get; set; } = string.Empty;

        [StringLength(50, MinimumLength = 1)]
        public string Genre { get; set; } = string.Empty;

        [StringLength(5, MinimumLength = 1)]
        [Required]
        public string Rating { get; set; } = string.Empty;

        [Range(1, 51420)] //Longest movie ever made
        [Required]
        public int RunTime { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Boolean NowPlaying { get; set; } //Added field to show nearby theatres playing this movie
    }
}