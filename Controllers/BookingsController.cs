using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CineCheck.Models;
using Microsoft.AspNetCore.Identity;

namespace CineCheck.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BookingsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                // Not logged in, redirect to login page
                return RedirectToAction("Login", "Account");
            }

            var bookings = await _context.Bookings
                .Include(b => b.Movie)
                .Include(b => b.Cinema)
                .Include(b => b.Showtime)
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return View(bookings);
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var booking = await _context.Bookings
                .Include(b => b.Cinema)
                .Include(b => b.Movie)
                .Include(b => b.Showtime)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null)
                return NotFound();

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["CinemaId"] = new SelectList(_context.Cinemas, "Id", "Name");
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title");
            ViewData["ShowtimeId"] = new SelectList(_context.Showtimes, "Id", "StartTime");
            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,CinemaId,ShowtimeId,NumberOfTickets")] Booking booking)
        {
            var userId = _userManager.GetUserId(User);
            Console.WriteLine($"DEBUG - Inside Create: UserId: {userId}");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            booking.UserId = userId;

            if (ModelState.IsValid)
            {
                Console.WriteLine("DEBUG - ModelState is valid, adding booking...");
                _context.Add(booking);
                await _context.SaveChangesAsync();
                Console.WriteLine("DEBUG - Booking saved!");
                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine("DEBUG - ModelState invalid!");
            ViewData["CinemaId"] = new SelectList(_context.Cinemas, "Id", "Name", booking.CinemaId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title", booking.MovieId);
            ViewData["ShowtimeId"] = new SelectList(_context.Showtimes, "Id", "StartTime", booking.ShowtimeId);
            return View(booking);
        }


        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return NotFound();

            ViewData["CinemaId"] = new SelectList(_context.Cinemas, "Id", "Name", booking.CinemaId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title", booking.MovieId);
            ViewData["ShowtimeId"] = new SelectList(_context.Showtimes, "Id", "StartTime", booking.ShowtimeId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,MovieId,CinemaId,ShowtimeId,NumberOfTickets")] Booking booking)
        {
            if (id != booking.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CinemaId"] = new SelectList(_context.Cinemas, "Id", "Name", booking.CinemaId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title", booking.MovieId);
            ViewData["ShowtimeId"] = new SelectList(_context.Showtimes, "Id", "StartTime", booking.ShowtimeId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var booking = await _context.Bookings
                .Include(b => b.Cinema)
                .Include(b => b.Movie)
                .Include(b => b.Showtime)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null)
                return NotFound();

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
