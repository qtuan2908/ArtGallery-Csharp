using ArtGallery.Data;
using ArtGallery.Models;
using ArtGallery.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ArtGallery.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly ApplicationDbContext _context;

        public CartController(CartService cartService, ApplicationDbContext context)
        {
            _cartService = cartService;
            _context = context;
            
        }
        public async Task<IActionResult> Index()
        {
            var accountId = User.Claims.FirstOrDefault(c => c.Type == "AccountId")?.Value;
            var cart = await _cartService.GetCart(int.Parse(accountId));
            var artworks = await _context.Artworks
                                         .Where(a => cart.ArtworkIds.Contains(a.ArtworkId))
                                         .ToListAsync();

            var cartView = new CartView { Artworks = artworks, CartId = cart.CartId };
            return View(cartView);
        }
        public async Task<IActionResult> Payment()
        {
            var accountId = User.Claims.FirstOrDefault(c => c.Type == "AccountId")?.Value;
            var cart = await _cartService.GetCart(int.Parse(accountId));
            var artworks = await _context.Artworks
                                         .Where(a => cart.ArtworkIds.Contains(a.ArtworkId))
                                         .ToListAsync();

            var cartView = new CartView { Artworks = artworks, CartId = cart.CartId };
            return View(cartView);
        }

        [HttpPost]
        public async Task<IActionResult> CheckoutConfirmed()
        {
            var accountId = User.Claims.FirstOrDefault(c => c.Type == "AccountId")?.Value;

            if (accountId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var cart = await _cartService.GetCart(int.Parse(accountId));
            var artworks = await _context.Artworks
                                         .Where(a => cart.ArtworkIds.Contains(a.ArtworkId))
                                         .ToListAsync();
           
            foreach (var artwork in artworks)
            {
                artwork.Status = Status.Sold;
                _context.Update(artwork);
            }
            await _context.SaveChangesAsync();
            await _cartService.ClearCart(int.Parse(accountId));

            TempData["SuccessMessage"] = "Payment successful! All artworks have been marked as Sold.";
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(int artworkId)
        {
            try
            {
                var accountId = User.Claims.FirstOrDefault(c => c.Type == "AccountId")?.Value;
                if (accountId == null) 
                {
                    throw new Exception("Account Invalid");
                }
                await _cartService.AddToCart(artworkId, int.Parse(accountId));
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index", "Artwork", new { artworkId });
            }
        }

        public async Task<IActionResult> RemoveFromCart(int artworkId)
        {
            var accountId = User.Claims.FirstOrDefault(c => c.Type == "AccountId")?.Value;
            await _cartService.RemoveFromCart(artworkId, int.Parse(accountId));
            return RedirectToAction("Index");
        }
    }
}
