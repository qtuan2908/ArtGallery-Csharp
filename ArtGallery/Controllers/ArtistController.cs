using ArtGallery.Data;
using ArtGallery.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ArtistController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ArtistController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var artists = await _context.Artists.Include(a => a.Account).ToListAsync();
            var artistViews = _mapper.Map<List<ArtistView>>(artists);

            return View(artistViews);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists
                .Include(a => a.Account)
                .FirstOrDefaultAsync(m => m.ArtistId == id);

            if (artist == null)
            {
                return NotFound();
            }

            var artistView = _mapper.Map<ArtistView>(artist);

            return View(artistView);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArtistCreate artistCreate)
        {
            if (ModelState.IsValid)
            {
                var account = _mapper.Map<Account>(artistCreate);
                account.Role = "Artist";
                _context.Add(account);
                await _context.SaveChangesAsync();

                var artist = _mapper.Map<Artist>(artistCreate);
                artist.AccountId = account.AccountId;
                _context.Add(artist);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(artistCreate);
        }

        //Get
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists.Include(a => a.Account).FirstOrDefaultAsync(x => x.ArtistId == id);

            if (artist == null)
            {
                return NotFound();
            }

            var artistView = _mapper.Map<ArtistView>(artist);

            return View(artistView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ArtistEdit artistEdit)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (ModelState.IsValid)
            {
                _mapper.Map(artistEdit, artist);
                _context.Update(artist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(artist);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists
                .Include(a => a.Account)
                .FirstOrDefaultAsync(m => m.ArtistId == id);
            if (artist == null)
            {
                return NotFound();
            }

            var artistView = _mapper.Map<ArtistView>(artist);

            return View(artistView);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(artist.AccountId);

            _context.Artists.Remove(artist);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ArtistExists(int id)
        {
            return _context.Artists.Any(e => e.ArtistId == id);
        }
    }
}

