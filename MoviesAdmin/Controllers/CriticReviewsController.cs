
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin;
using MoviesAdmin.Models;


namespace MoviesAdmin.Controllers
{
    [Authorize]
    public class CriticReviewsController : Controller
    {

        private readonly MoviesAdminContext _context;

        private string? _userId;

        private bool CanModify(CriticReview review)
        {
            return User.IsInRole("admin") || !review.IsPublished;
        }

        public CriticReviewsController(MoviesAdminContext context)
        {
            _context = context;

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            base.OnActionExecuting(context);

            var _userId = User.GetUserId();
            if (_userId != null)
            {
                Console.WriteLine("Logged in user: " + _userId);
            }
        }

        // GET: CRITICREVIEWS
        public async Task<IActionResult> Index()
        {
            var user = User.GetUserId();

            var criticReviews = await _context.CriticReview
                .Include(m => m.Movie)
                .ToListAsync();

            if (User.IsInRole("critic"))
            {
                criticReviews = await _context.CriticReview
                .Where(m => m.CreatedBy == user)
                .Include(m => m.Movie)
                .ToListAsync();
            }

            return View(criticReviews);
        }

        // GET: CRITICREVIEWS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criticreview = await _context.CriticReview
                .FirstOrDefaultAsync(m => m.ID == id);
            if (criticreview == null)
            {
                return NotFound();
            }

            return View(criticreview);
        }

        // GET: CRITICREVIEWS/Create
        public IActionResult Create()
        {
            var movies = _context.Movie.ToList();
            ViewBag.MovieID = new SelectList(movies, "ID", "Title");
            return View();
        }

        // POST: CRITICREVIEWS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MovieID,Description,Rating,IsPublished,CreatedBy,CreatedDate")] CriticReview criticreview)
        {
            var user = User.GetUserId();
            criticreview.CreatedBy = user;

            bool duplicate = await _context.CriticReview
                .AnyAsync(r => r.MovieID == criticreview.MovieID && r.CreatedBy == user);

            if (duplicate)
            {
                ModelState.AddModelError(nameof(criticreview.MovieID), "You've already reviewed this movie.");
            }

            if (user != null)
            {
                Console.WriteLine("Logged in user: " + _userId);
            }

            if (ModelState.IsValid)
            {
                _context.Add(criticreview);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.MovieID = new SelectList(_context.Movie, "ID", "Title", criticreview.MovieID);
            return View(criticreview);
        }

        // GET: CRITICREVIEWS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var criticreview = await _context.CriticReview
                .Include(r => r.Movie)
                .FirstOrDefaultAsync(r => r.ID == id);
            if (criticreview == null || !CanModify(criticreview))
            {
                return NotFound();
            }
            return View(criticreview);
        }

        // POST: CRITICREVIEWS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ID,Description,Rating,IsPublished")] CriticReview criticreview)
        {
            if (id != criticreview.ID)
            {
                return NotFound();
            }

            var existing = await _context.CriticReview
            .Include(r => r.Movie)
            .FirstOrDefaultAsync(r => r.ID == id);

            if (ModelState.IsValid)
            {
                existing.Description = criticreview.Description;
                existing.Rating = criticreview.Rating;
                existing.IsPublished = criticreview.IsPublished;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            criticreview.Movie = existing.Movie;
            return View(criticreview);
        }

        // GET: CRITICREVIEWS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criticreview = await _context.CriticReview
                .FirstOrDefaultAsync(m => m.ID == id);
            if (criticreview == null || !CanModify(criticreview))
            {
                return NotFound();
            }

            return View(criticreview);
        }

        // POST: CRITICREVIEWS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var criticreview = await _context.CriticReview.FindAsync(id);
            if (criticreview != null)
            {
                _context.CriticReview.Remove(criticreview);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CriticReviewExists(int? id)
        {
            return _context.CriticReview.Any(e => e.ID == id);
        }
    }
}