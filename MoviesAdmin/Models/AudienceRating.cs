using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MoviesAdmin.Models
{
    public class AudienceRating
    {
        //PK
        public int ID { get; set; }

        //nav property
        [ValidateNever]
        public List<Movie> Movie { get; set; } = new List<Movie>();


        public string Title { get; set; } = string.Empty;
    }
}
