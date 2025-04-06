using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CloudPOE.Data;
using CloudPOE.Models;

namespace CloudPOE.Controllers
{
    public class BookingsController : Controller
    {
        private readonly CloudPOEContext _context;

        public BookingsController(CloudPOEContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var cloudPOEContext = _context.Bookings.Include(b => b.Events).Include(b => b.Venue);
            return View(await cloudPOEContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .Include(b => b.Events)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(m => m.bookingID == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["EventID"] = new SelectList(_context.Event, "EventID", "EventName");
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "VenueName");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("bookingID,EventID,VenueID,bookingDate")] Bookings bookings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventID"] = new SelectList(_context.Event, "EventName", "EventID", bookings.EventID);
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueName", "VenueID", bookings.VenueID);
            return View(bookings);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings == null)
            {
                return NotFound();
            }
            ViewData["EventID"] = new SelectList(_context.Event, "EventID", "EventID", bookings.EventID);
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "VenueID", bookings.VenueID);
            return View(bookings);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("bookingID,EventID,VenueID,bookingDate")] Bookings bookings)
        {
            if (id != bookings.bookingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingsExists(bookings.bookingID))
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
            ViewData["EventID"] = new SelectList(_context.Event, "EventID", "EventID", bookings.EventID);
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "VenueID", bookings.VenueID);
            return View(bookings);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .Include(b => b.Events)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(m => m.bookingID == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings != null)
            {
                _context.Bookings.Remove(bookings);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingsExists(int id)
        {
            return _context.Bookings.Any(e => e.bookingID == id);
        }
    }
}
