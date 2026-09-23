namespace MoviesAdmin.Models
{
    public class Theatre
    {
        public int Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public int Theatres { get; set; }
        public string[] NowPlaying { get; set; } = Array.Empty<string>();
    }
}
