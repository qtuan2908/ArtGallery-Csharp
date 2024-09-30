using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGallery.Models
{
    public class CartView
    {
        [Key]
        public int CartId { get; set; }
        [Required]
        public List<Artwork> Artworks { get; set; }
    }
}
