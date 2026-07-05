using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fixly.Models
{
    public class ServiceProviderProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Service category is required.")]
        [StringLength(50, ErrorMessage = "Service category cannot exceed 50 characters.")]
        public string ServiceCategory { get; set; }

        [Required(ErrorMessage = "Years of experience is required.")]
        [Range(0, 60, ErrorMessage = "Invalid number of years of experience.")]
        public int YearsExperience { get; set; }

        [StringLength(1000, ErrorMessage = "About section cannot exceed 1000 characters.")]
        public string About { get; set; }

        public ICollection<WorkImage> WorkImages { get; set; }
    }
}