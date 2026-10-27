
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin.Models;
using MoviesAdmin.Services;

public class MoviesController : Controller
{
    private readonly MoviesAdminContext _context;

    private readonly AzureStorageService _blobService;

    public MoviesController(MoviesAdminContext context, AzureStorageService blobService)
    {
        _context = context;
        _blobService = blobService;
    }

    // GET: MOVIES
    public async Task<IActionResult> Index(int page = 1)
    {
        int pageSize = 3;//sets table limit for movies per page

        var movies = await _context.Movie
            .Include(m => m.Genre)
            .OrderByDescending(m => m.ReleaseDate)
            .Skip((page - 1) * pageSize)//calcs how many rows to skip
            .Take(pageSize)//displays the correct amount of movies at the correct starting point
            .ToListAsync();//gets all movies in DB

        int totalMovies = await _context.Movie.CountAsync();//stores the value of all movie DB entries
        int totalPages = (int)Math.Ceiling(totalMovies / (double)pageSize);//Calculates how many needed pages

        ViewBag.Page = page;
        ViewBag.TotalPages = totalPages;//used for pagination in the view

        return View(movies);//sends the paged movie list to the Razor view
    }

    // GET: MOVIES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .Include(m => m.Genre)
            .FirstOrDefaultAsync(m => m.ID == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // GET: MOVIES/Create
    public IActionResult Create()
    {
        var genres = _context.Genre.ToList();
        ViewBag.GenreID = new SelectList(genres, "ID", "Title");
        return View();
    }

    // POST: MOVIES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
    [Bind("ID,GenreID,AudienceRatingID,CriticReviewID,Title,Synopsis,RunTime,ReleaseDate,NowPlaying")] Movie movie,
    IFormFile ImageFile)
    {
        if (ImageFile == null || ImageFile.Length == 0)
        {
            ModelState.AddModelError("ImageFile", "Please select an image.");
        }

        if (ModelState.IsValid)
        {
            //save record in db
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(ImageFile.FileName)}";
            string imageUrl = await _blobService.UploadFileAsync(ImageFile, fileName);
            movie.ImageUrl = imageUrl;
            _context.Add(movie);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    // GET: MOVIES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        return View(movie);
    }

    // POST: MOVIES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("ID,GenreID,AudienceRatingID,CriticReviewID,Title,Synopsis,RunTime,ReleaseDate,NowPlaying")] Movie movie, IFormFile? ImageFile)
    {
        if (id != movie.ID)
        {
            return NotFound();
        }

        var existingMovie = await _context.Movie.FindAsync(id);

        if (existingMovie == null) { 
            return NotFound();
        }

        existingMovie.Title = movie.Title;
        existingMovie.Synopsis = movie.Synopsis;
        existingMovie.Genre = movie.Genre;
        existingMovie.AudienceRating = movie.AudienceRating;
        existingMovie.RunTime = movie.RunTime;
        existingMovie.ReleaseDate = movie.ReleaseDate;
        existingMovie.NowPlaying = movie.NowPlaying;

        if (ModelState.IsValid)
        {
            try
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(ImageFile.FileName)}";

                    string imageUrl =
                        await _blobService.UploadFileAsync(ImageFile, fileName);

                    existingMovie.ImageUrl = imageUrl;
                }
                
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    // GET: MOVIES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.ID == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: MOVIES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var movie = await _context.Movie.FindAsync(id);
        if (movie != null)
        {
            _context.Movie.Remove(movie);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int? id)
    {
        return _context.Movie.Any(e => e.ID == id);
    }
}
