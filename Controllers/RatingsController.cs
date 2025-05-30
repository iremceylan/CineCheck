using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CineCheck.Models;

namespace CineCheck.Controllers
{
    public class RatingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RatingsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int movieId, int score)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var rating = new Rating
            {
                MovieId = movieId,
                UserId = userId,
                Score = score
            };

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Movies");
        }
    }
}

