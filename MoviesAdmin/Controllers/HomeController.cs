using Microsoft.AspNetCore.Mvc;
using MoviesAdmin.Models;
using System.Diagnostics;

namespace MoviesAdmin.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            Console.WriteLine("Hello from the Contructor");
            
        }
        public IActionResult Index()
        {
            Console.WriteLine("Hello from the Home/Index controller");
            return View();
        }

        public IActionResult Privacy()
        {
            Console.WriteLine("Hello from the Home/Privacy controller");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
