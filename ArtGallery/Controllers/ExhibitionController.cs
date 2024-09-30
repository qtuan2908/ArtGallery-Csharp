using ArtGallery.Data;
using ArtGallery.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Web;

namespace ArtGallery.Controllers
{
    [Authorize]
    public class ExhibitionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExhibitionController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["ActiveNav"] = "Exhibition";
            var exhibitions = await _context.Exhibitions.ToListAsync();
            var exhibitionViews = _mapper.Map<List<ExhibitionView>>(exhibitions);
            return View(exhibitionViews);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Admin()
        {
            var exhibitions = await _context.Exhibitions.ToListAsync();
            var exhibitionViews = _mapper.Map<List<ExhibitionView>>(exhibitions);
            return View(exhibitionViews);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var exhibition = await _context.Exhibitions.FirstOrDefaultAsync(e => e.ExhibitionId == id);
            if (exhibition == null)
            {
                return NotFound();
            }
            var exhibitionView = _mapper.Map<ExhibitionView>(exhibition);            
            exhibitionView.ArtworkImages = await _context.Artworks.Where(x => x.ExhibitionId == id).Select(x => x.ImageURL).ToListAsync();
            return View(exhibitionView);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exhibition/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExhibitionView exhibitionView)
        {

            if (ModelState.IsValid)
            {
                var exhibition = _mapper.Map<Exhibition>(exhibitionView);
                _context.Add(exhibition);
                await _context.SaveChangesAsync();
                return RedirectToAction("Admin");
            }
            return View(exhibitionView);
        }

        //GET: Exhibition/Edit
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var exhibition = await _context.Exhibitions.FindAsync(id);
            if (exhibition == null)
            {
                return NotFound();
            }
            var exhibitionView = _mapper.Map<ExhibitionView>(exhibition);
            return View(exhibitionView);
        }

        //POST: Exhibition/Edit
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ExhibitionView exhibitionView)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exhibition = await _context.Exhibitions.FindAsync(id);
            if (exhibition == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    exhibitionView.ExhibitionId = exhibition.ExhibitionId;
                    _mapper.Map(exhibitionView, exhibition);

                    _context.Update(exhibition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExhibitionExists(exhibitionView.ExhibitionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Admin");
            }
            return View(exhibitionView);
        }

        //GET: Exhibition/Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var exhibition = await _context.Exhibitions.FindAsync(id);
            if (exhibition == null)
            {
                return NotFound();
            }
            var exhibitionView = _mapper.Map<ExhibitionView>(exhibition);
            return View(exhibitionView);
        }

        //POST: Exhibition/Delete
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exhibition = await _context.Exhibitions.FindAsync(id);
            _context.Exhibitions.Remove(exhibition);
            await _context.SaveChangesAsync();
            return RedirectToAction("Admin");
        }
        private bool ExhibitionExists(int id)
        {
            return _context.Exhibitions.Any(e => e.ExhibitionId == id);
        }
    }
}
