using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGallery.Models
{
    public class AuctionCreate
    {
        public int ArtworkId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double StartingPrice { get; set; }
    }
}
