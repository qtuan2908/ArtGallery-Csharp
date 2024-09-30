using Microsoft.AspNetCore.Authorization;
using ArtGallery.Data;
using ArtGallery.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ArtGallery.Controllers
{
    [Authorize]
    public class AuctionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AuctionController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["ActiveNav"] = "Auction";
            var auctions = await _context.Auctions
                .Include(a => a.Artwork)
                .Include(a => a.Account)
                .ToListAsync();

            auctions.Reverse();
            var auctionViews = _mapper.Map<List<AuctionView>>(auctions);

            return View(auctionViews);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Admin()
        {
            var auctions = await _context.Auctions
                .Include(a => a.Artwork)
                .Include(a => a.Account)
                .ToListAsync();

            var auctionViews = _mapper.Map<List<AuctionView>>(auctions);

            return View(auctionViews);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceBid(AuctionView model)
        {
            var auction = await _context.Auctions.FindAsync(model.AuctionId);

            if (auction == null)
            {
                return NotFound();
            }

            if (model.NewBid <= auction.CurrentBid)
            {
                ModelState.AddModelError("", "Your bid must be higher than the current bid.");
                var auctions = await _context.Auctions
                    .Include(a => a.Artwork)
                    .Include(a => a.Account)
                    .ToListAsync();
                var auctionViews = _mapper.Map<List<AuctionView>>(auctions);
                return View("Index", auctionViews);
                //ModelState.AddModelError("", "Your bid must be higher than the current bid.");
                //return View("Index", model);
            }
            if (model.NewBid <= auction.StartingPrice)
            {
                ModelState.AddModelError("", "Your bid must be higher than the Start price.");
                var auctions = await _context.Auctions
                    .Include(a => a.Artwork)
                    .Include(a => a.Account)
                    .ToListAsync();
                var auctionViews = _mapper.Map<List<AuctionView>>(auctions);
                return View("Index", auctionViews);
                //ModelState.AddModelError("", "Your bid must be higher than the current bid.");
                //return View("Index", model);
            }

            auction.CurrentBid = model.NewBid;
            auction.AccountId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "AccountId")?.Value);

            _context.Auctions.Update(auction);
            
            var artwork = await _context.Artworks.FindAsync(auction.ArtworkId);
            artwork.Status = Status.Sold;
            _context.Update(artwork);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Artwork = await _context.Artworks
                .Where(x => x.Status == Status.Available && x.Category == Category.Auction)
                .ToListAsync();
            return View();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuctionCreate auctionCreate)
        {
            if (ModelState.IsValid)
            {
                var auction = _mapper.Map<Auction>(auctionCreate);
                _context.Add(auction);
                await _context.SaveChangesAsync();

                return RedirectToAction("Admin");
            }

            ViewBag.Artwork = await _context.Artworks.ToListAsync();
            return View(auctionCreate);
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var auction = await _context.Auctions.FindAsync(id);

            if (auction == null)
            {
                return NotFound();
            }
            var auctionViewModel = _mapper.Map<AuctionEdit>(auction);
            ViewBag.Artwork = await _context.Artworks.ToListAsync();
            return View(auctionViewModel);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AuctionEdit auctionEdit)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await _context.Auctions.FindAsync(id);
            if (auction == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _mapper.Map(auctionEdit, auction);
                _context.Update(auction);
                await _context.SaveChangesAsync();
                return RedirectToAction("Admin");
            }
            return View(auctionEdit);
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var auction = await _context.Auctions
                .Include(a => a.Artwork)
                .FirstOrDefaultAsync(x => x.AuctionId == id);

            if (auction == null)
            {
                return NotFound();
            }

            var auctionView = _mapper.Map<AuctionView>(auction);
            return View(auctionView);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auction = await _context.Auctions.FindAsync(id);
            if (auction == null)
            {
                return NotFound();
            }

            _context.Auctions.Remove(auction);

            var artwork = await _context.Artworks.FindAsync(auction.ArtworkId);
            artwork.Status = Status.Available;
            _context.Update(artwork);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Admin));
        }
    }
}
