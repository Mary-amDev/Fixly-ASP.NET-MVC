namespace Fixly.Models
{
    // Used only to DISPLAY the profile (read-only page)
    public class ProfileViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string ProfilePicturePath { get; set; }
        public string Role { get; set; } // "Customer" or "Service Provider"

        // Only filled in when Role is "Service Provider"
        public string ServiceCategory { get; set; }
        public int YearsExperience { get; set; }
        public string About { get; set; }
        public List<WorkImage> WorkImages { get; set; }
    }
}