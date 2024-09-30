using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ArtGallery.Validations;
namespace ArtGallery.Models
{
    public class Artwork
    {
        [Key]
        public int ArtworkId { get; set; }
        [Required]
        public int ArtistId { get; set; }
        public int? ExhibitionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        [Required]
        [CategoryValidation]
        [CategoryForSaleStatusValidation]
        public Category Category { get; set; }
        public double? Price { get; set; }
        [Required]
        public Status Status { get; set; }
        public DateTime? CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; } = DateTime.Now;
        [ForeignKey("ArtistId")]
        public Artist Artist { get; set; }
        [ForeignKey("ExhibitionId")]
        public Exhibition Exhibition { get; set; }
    }
    public enum Category
    {
        ForSale,
        Auction
    }
    public enum Status
    {
        Available,
        Sold
    }
}
