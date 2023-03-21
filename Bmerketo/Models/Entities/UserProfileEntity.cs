using Microsoft.AspNetCore.Identity;

namespace Bmerketo.Models.Entities
{
    public class UserProfileEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } = null!;   
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? StreetName { get; set; } 
        public string? PostalCode { get; set; } 
        public string? City { get; set; } 
        public string? PhoneNumber { get; set; }
        public string? Company { get; set; }
        public string? ImageName { get; set; }       
        public DateTime CreatedAt { get; set; }
        public virtual IdentityUser User { get; set; } = null!;
    }
}
