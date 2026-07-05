using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Fixly.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "يجب إدخال الاسم")]
        [StringLength(100, ErrorMessage = "الاسم يجب ألا يتجاوز 100 حرف")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "يجب إدخال المدينة")]
        [StringLength(50, ErrorMessage = "اسم المدينة يجب ألا يتجاوز 50 حرف")]
        public string City { get; set; }
    }
}