using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public DbSet<Artist> Artists { get; set; }
		public DbSet<Artwork> Artworks { get; set; }
		public DbSet<Auction> Auctions { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Exhibition> Exhibitions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ArtGallery.Models.ExhibitionDetail> ExhibitionDetail { get; set; } = default!;

    }
}



