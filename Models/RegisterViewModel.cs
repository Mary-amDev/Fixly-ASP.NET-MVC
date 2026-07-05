using System.ComponentModel.DataAnnotations;

namespace Fixly.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "يجب إدخال الاسم الكامل")]
        [StringLength(100, ErrorMessage = "الاسم يجب ألا يتجاوز 100 حرف")]
        [Display(Name = "الاسم الكامل")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "يجب إدخال المدينة")]
        [StringLength(50, ErrorMessage = "اسم المدينة يجب ألا يتجاوز 50 حرف")]
        [Display(Name = "المدينة")]
        public string City { get; set; }

        [Required(ErrorMessage = "يجب إدخال رقم الجوال")]
        [RegularExpression(@"^05\d{8}$", ErrorMessage = "رقم الجوال غير صحيح، يجب أن يبدأ بـ 05 ويتكون من 10 أرقام")]
        [Display(Name = "رقم الجوال")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "يجب إدخال البريد الإلكتروني")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة")]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }

        [Required(ErrorMessage = "يجب إدخال كلمة المرور")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "كلمة المرور يجب ألا تقل عن 6 أحرف")]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        [Required(ErrorMessage = "يجب تأكيد كلمة المرور")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "كلمة المرور وتأكيدها غير متطابقين")]
        [Display(Name = "تأكيد كلمة المرور")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "يجب اختيار نوع الحساب")]
        [Display(Name = "نوع الحساب")]
        public string Role { get; set; }

        [Display(Name = "التخصص")]
        public string? ServiceCategory { get; set; }

        public string? About { get; set; }

        public int? YearsExperience { get; set; }
    }
}