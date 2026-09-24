using Microsoft.AspNetCore.Mvc;
using MoviesAdmin.Models;
using System.Diagnostics;

namespace MoviesAdmin.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }
        public IActionResult Index()
        {

            return View(new LoginCred());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Movies()
        {
            List<Movie> movies = new List<Movie> { 
            
                new Movie
                {
                    ID = 1,
                    Title = "Silence of the Lambs",
                    Synopsis = "Clarese spspspspspspspsspsps",
                    Genre = "Thriller/Horror",
                    Rating = "R",
                    RunTime = "2h",
                    ReleaseDate = "1998",
                    NowPlaying = false,
                },
                new Movie
                {
                    ID = 2,
                    Title = "Inception",
                    Synopsis = "BP is dreamy and loves dradles",
                    Genre = "Thriller",
                    Rating = "PG-14",
                    RunTime = "3h",
                    ReleaseDate = "2011",
                    NowPlaying = false,
                },
                new Movie
                {
                    ID = 3,
                    Title = "Starwars",
                    Synopsis = "small boy grows up and enslaves a galaxy",
                    Genre = "Sci-Fi",
                    Rating = "PG",
                    RunTime = "1h30m",
                    ReleaseDate = "1973",
                    NowPlaying = false,
                },
            };
            return View(movies);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
