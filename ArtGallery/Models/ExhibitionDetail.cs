using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGallery.Models
{
    public class ExhibitionDetail
    {
        [Key]
        public int ExhibitionDetailId { get; set; }
        [Required]
        public int ExhibitionId { get; set; }
        public int ArtworkId { get; set; }
        [ForeignKey("ExhibitionId")]
        public Exhibition Exhibition { get; set; }
        [ForeignKey("ArtworkId")]
        public Artwork ArtWork { get; set; }
    }
}
