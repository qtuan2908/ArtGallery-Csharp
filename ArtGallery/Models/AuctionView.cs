using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGallery.Models
{
    public class AuctionView
    {
        public int AuctionId { get; set; }
        public int ArtworkId { get; set; }
        public string ArtworkTitle { get; set; }
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double StartingPrice { get; set; }
        public double CurrentBid { get; set; }
        [Required]
        public string UserName { get; set; }
        public double NewBid { get; set; }
    }
}
