using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGallery.Models
{
    public class ArtworkView
    {
        public int ArtworkId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public Category Category { get; set; }
        public Status Status { get; set; }
        public double? Price { get; set; }
        public string ArtistName { get; set; }
        public DateTime? CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; } = DateTime.Now;
    }
}
