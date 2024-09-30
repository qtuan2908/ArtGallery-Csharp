using System.ComponentModel.DataAnnotations;

namespace ArtGallery.Models
{
    public class Exhibition
    {
        [Key]
        public int ExhibitionId { get; set; }
        [Required]
        public string ExhibitionName { get; set; }
        public string ImageUrl { get; set; }
        public string Venue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
}
