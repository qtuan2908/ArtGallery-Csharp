using System.ComponentModel.DataAnnotations;

namespace ArtGallery.Models
{
    public class Register
    {
        public string Role { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string? ArtistName { get; set; }
        public string? Nationality { get; set; }
        public string? Email { get; set; }
        public string? Biography { get; set; }
        public string? Website { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
    }
}
