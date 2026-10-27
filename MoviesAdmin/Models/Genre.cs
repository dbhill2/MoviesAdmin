using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Genre
    {
        public int ID { get; set; }

        //nav property
        [ValidateNever]
        public List<Movie> Movie { get; set; } = new List<Movie>();

        public string Title { get; set; } = string.Empty;
    }
}
