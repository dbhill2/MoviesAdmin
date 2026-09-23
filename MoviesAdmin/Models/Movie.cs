namespace MoviesAdmin.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Synopsis { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Rating { get; set; } = string.Empty;
        public string RunTime { get; set; } = string.Empty;
        public string ReleaseDate { get; set; } = string.Empty;
    }
}
