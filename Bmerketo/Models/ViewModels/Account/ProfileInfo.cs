using System.ComponentModel.DataAnnotations;

namespace Bmerketo.Models.ViewModels.Account
{
    public class ProfileInfo
    {
        [Key]
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string StreetName { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string? Company { get; set; }
        public string? ReturnUrl { get; set; }
        public string? Role { get; set; }
        public string? ProfileImage { get; set; }
        public IFormFile? GetProfileImage { get; set; }
    }
}
