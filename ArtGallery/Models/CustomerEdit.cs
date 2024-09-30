using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGallery.Models
{
    public class CustomerEdit
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
