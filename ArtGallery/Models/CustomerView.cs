using System.ComponentModel.DataAnnotations;

namespace ArtGallery.Models
{
    public class CustomerView
    {
        public int CustomerId { get; set; }
        [Required]
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
