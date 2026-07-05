using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fixly.Models
{
    public enum RequestStatus
    {
        Pending,
        Accepted,
        Rejected,
        Completed
    }

    public class ServiceRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public ApplicationUser Customer { get; set; }

        [Required]
        public string ProviderId { get; set; }

        [ForeignKey("ProviderId")]
        public ApplicationUser Provider { get; set; }

        [Required(ErrorMessage = "يجب إدخال وصف المشكلة")]
        [StringLength(500, ErrorMessage = "وصف المشكلة يجب ألا يتجاوز 500 حرف")]
        public string ProblemDescription { get; set; }

        [Required(ErrorMessage = "يجب إدخال التاريخ المطلوب")]
        [DataType(DataType.Date)]
        public DateTime RequestedDate { get; set; }

        [Required(ErrorMessage = "يجب إدخال الوقت المطلوب")]
        [DataType(DataType.Time)]
        public TimeSpan RequestedTime { get; set; }

        [Required]
        public RequestStatus Status { get; set; } = RequestStatus.Pending;
    }
}