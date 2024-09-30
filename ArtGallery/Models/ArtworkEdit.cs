using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ArtGallery.Validations;
namespace ArtGallery.Models
{
    public class ArtworkEdit
    {
        [Required]
        public int ArtworkId { get; set; }
        [Required]
        public int ArtistId { get; set; }
        public int? ExhibitionId { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public double? Price { get; set; }
        [Required]
        public Status Status { get; set; }
    }
}
