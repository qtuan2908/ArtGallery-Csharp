using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGallery.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        [Required]
        public List<int> ArtworkIds { get; set; }
        [Required]
        public int AccountId { get; set; }
    }
}
