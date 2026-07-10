using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Fixly.Models
{
    // Used only for the EDIT form
    public class ProfileEditViewModel
    {
        public string Role { get; set; } 

        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50)]
        public string City { get; set; }

        // The picture already saved, and an optional new one to replace it
      public string? CurrentProfilePicturePath { get; set; }
      public IFormFile? ProfilePicture { get; set; }
        // Only used when Role is "Service Provider"
        [StringLength(50)]
        public string? ServiceCategory { get; set; }

        [Range(0, 60, ErrorMessage = "Invalid number of years of experience.")]
        public int YearsExperience { get; set; }

        [StringLength(1000)]
        public string? About { get; set; }

        public List<WorkImage>? ExistingWorkImages { get; set; }
        public List<IFormFile>? NewWorkImages { get; set; }
    }
}