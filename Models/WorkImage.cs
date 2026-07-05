using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fixly.Models
{
    public class WorkImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ServiceProviderProfileId { get; set; }

        [ForeignKey("ServiceProviderProfileId")]
        public ServiceProviderProfile ServiceProviderProfile { get; set; }

        [Required(ErrorMessage = "يجب إدخال مسار الصورة")]
        public string ImagePath { get; set; }
    }
}