
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

        private string _userId = string.Empty;

        private bool IsOwner(CriticReview r) => r.CreatedBy == _userId; //helper to match owner of review to current user used in CanModify checker

        private bool CanModify(CriticReview review)//checks role/if the user owns the review or if the review is published
        {
            return User.IsInRole("admin") || (IsOwner(review) && !review.IsPublished);
        }

        private bool CanView(CriticReview r) => User.IsInRole("admin") || IsOwner(r);//checks the role and owner only

        public CriticReviewsController(MoviesAdminContext context)
        {
            _context = context;

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            base.OnActionExecuting(context);

            _userId = User.GetUserId();
            Console.WriteLine("Logged in user: " + _userId);
        }

        // GET: CRITICREVIEWS
        public async Task<IActionResult> Index()
        {
            var query = _context.CriticReview.Include(r => r.Movie).AsQueryable();

            if (!User.IsInRole("admin"))
                query = query.Where(r => r.CreatedBy == _userId);

            return View(await query.OrderByDescending(r => r.CreatedDate).ToListAsync());
        }

        // GET: CRITICREVIEWS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criticreview = await _context.CriticReview
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (criticreview == null || !CanView(criticreview))
            {
                return NotFound();
            }

            return View(criticreview);
        }

        // GET: CRITICREVIEWS/Create
        public IActionResult Create(int? movieId)//used to get the movie Id for a critic to review from details page
        {
            var movies = _context.Movie.ToList();
            ViewBag.MovieID = new SelectList(movies, "ID", "Title", movieId);
            return View(new CriticReview { MovieID = movieId ?? 0 });
        }

        // POST: CRITICREVIEWS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MovieID,Description,Rating,IsPublished,CreatedBy,CreatedDate")] CriticReview criticreview)
        {
            criticreview.CreatedBy = _userId;

            bool duplicate = await _context.CriticReview
                .AnyAsync(r => r.MovieID == criticreview.MovieID && r.CreatedBy == _userId);

            if (duplicate)
            {
                ModelState.AddModelError(nameof(criticreview.MovieID), "You've already reviewed this movie.");
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

            if (existing == null || !CanModify(existing))
            {
                return NotFound();
            }

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
                .Include(m => m.Movie)
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
            if (criticreview == null || !CanModify(criticreview))
            {
                return NotFound();
            }

            _context.CriticReview.Remove(criticreview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CriticReviewExists(int? id)
        {
            return _context.CriticReview.Any(e => e.ID == id);
        }
    }
}