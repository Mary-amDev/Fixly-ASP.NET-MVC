using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Fixly.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City name cannot exceed 50 characters.")]
        public string City { get; set; }

        public string? ProfilePicturePath { get; set; }
    }
}