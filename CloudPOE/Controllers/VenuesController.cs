using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CloudPOE.Data;
using CloudPOE.Models;
using CloudPOE.Service;

namespace CloudPOE.Controllers
{
    public class VenuesController : Controller
    {
        private readonly CloudPOEContext _context;

        private readonly BlobService _blobService;

        //public VenuesController(CloudPOEContext context)
        //{
        //    _context = context;
        //}

        public VenuesController(CloudPOEContext context, BlobService blobService)
        {
            _context = context;
            _blobService = blobService;
        }

        // GET: Venues
        public async Task<IActionResult> Index()
        {
            return View(await _context.Venues.ToListAsync());
        }

        // GET: Venues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venues = await _context.Venues
                .FirstOrDefaultAsync(m => m.VenueID == id);
            if (venues == null)
            {
                return NotFound();
            }

            return View(venues);
        }

        // GET: Venues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Venues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VenueID,VenueName,location,Capacity,URLImage")] Venues venues, IFormFile file)
        {
            if (file != null)
            {
                var fileName = file.FileName;
                var blobExists = await _blobService.BlobExistsAsync(fileName);

                //step 11
                if (blobExists)
                {
                    ModelState.AddModelError("URLImage", "Image has been uploaded previously.");
                    return View(venues);
                }
                using var stream = file.OpenReadStream();
                var blob = await _blobService.uploadAsync(stream, file.FileName);
                venues.URLImage = blob;
            }

            if (ModelState.IsValid)
            {
                _context.Add(venues);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venues);
        }

        // GET: Venues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venues = await _context.Venues.FindAsync(id);
            if (venues == null)
            {
                return NotFound();
            }
            return View(venues);
        }

        // POST: Venues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VenueID,VenueName,location,Capacity,URLImage")] Venues venues)
        {
            if (id != venues.VenueID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venues);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenuesExists(venues.VenueID))
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
            return View(venues);
        }

        // GET: Venues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue != null)
            {
                // Delete the image from Azure Blob Storage
                if (!string.IsNullOrEmpty(venue.URLImage))
                {
                    // Extract the blob name from the URL 
                    var blobName = Path.GetFileName(new Uri(venue.URLImage).LocalPath);
                    await _blobService.DeleteBlobAsync(blobName);
                }

                _context.Venues.Remove(venue);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venues = await _context.Venues.FindAsync(id);
            if (venues != null)
            {
                _context.Venues.Remove(venues);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenuesExists(int id)
        {
            return _context.Venues.Any(e => e.VenueID == id);
        }
    }
}
