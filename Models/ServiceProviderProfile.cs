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

        [Required(ErrorMessage = "يجب إدخال التخصص")]
        [StringLength(50, ErrorMessage = "التخصص يجب ألا يتجاوز 50 حرف")]
        public string ServiceCategory { get; set; }

        [Required(ErrorMessage = "يجب إدخال عدد سنوات الخبرة")]
        [Range(0, 60, ErrorMessage = "عدد سنوات الخبرة غير صحيح")]
        public int YearsExperience { get; set; }

        [StringLength(1000, ErrorMessage = "النبذة يجب ألا تتجاوز 1000 حرف")]
        public string About { get; set; }

        public ICollection<WorkImage> WorkImages { get; set; }
    }
}