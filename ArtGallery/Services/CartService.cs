using ArtGallery.Data;
using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ArtGallery.Services
{
    public class CartService
    {
        private readonly ApplicationDbContext _context;
        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> GetCartCount(int accountId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.AccountId == accountId);
            if (cart == null || cart.ArtworkIds == null)
            {
                return 0;
            }
            return cart.ArtworkIds.Count();
        }
        public async Task AddToCart(int artworkId, int accountId)
        {
            var artwork = await _context.Artworks.FindAsync(artworkId);

            if (artwork == null || artwork.Category == Models.Category.Auction || artwork.Status == Models.Status.Sold)
            {
                throw new InvalidOperationException("Artwork cannot be added to cart.");
            }

            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.AccountId == accountId);

            if (cart == null)
            {
                cart = new Cart
                {
                    AccountId = accountId,
                    ArtworkIds = [artworkId]
                };
                _context.Carts.Add(cart);
            }
            else
            {
                if (!cart.ArtworkIds.Contains(artworkId))
                {
                    cart.ArtworkIds.Add(artworkId);
                }
                _context.Carts.Update(cart);
            }
            await _context.SaveChangesAsync();
        }
        public async Task RemoveFromCart(int artworkId, int accountId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.AccountId == accountId);

            if (cart != null)
            {
                cart.ArtworkIds.Remove(artworkId);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Cart> GetCart(int accountId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.AccountId == accountId);

            if (cart == null)
            {
                cart = new Cart
                {
                    AccountId = accountId,
                    ArtworkIds = []
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }
            return cart;
        }
        public async Task ClearCart(int accountId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.AccountId == accountId);

            if (cart != null)
            {
                cart.ArtworkIds.Clear();  // Xóa tất cả các artwork trong giỏ hàng
                await _context.SaveChangesAsync();
            }
        }

    }
}
