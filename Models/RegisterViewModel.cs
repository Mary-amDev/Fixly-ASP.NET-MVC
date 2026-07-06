using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Fixly.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City name cannot exceed 50 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^05\d{8}$", ErrorMessage = "Invalid phone number. It must start with 05 and contain 10 digits.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please select an account type.")]
        public string Role { get; set; }

        public string? ServiceCategory { get; set; }

        public string? About { get; set; }

        public int? YearsExperience { get; set; }
        
        public List<IFormFile>? WorkImages { get; set; }
    }
}