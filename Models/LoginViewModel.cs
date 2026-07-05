using System.ComponentModel.DataAnnotations;

namespace Fixly.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "يجب إدخال البريد الإلكتروني")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة")]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }

        [Required(ErrorMessage = "يجب إدخال كلمة المرور")]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        [Display(Name = "تذكرني")]
        public bool RememberMe { get; set; }
    }
}