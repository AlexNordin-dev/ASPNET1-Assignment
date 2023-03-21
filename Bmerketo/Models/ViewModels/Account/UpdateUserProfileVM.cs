using System.ComponentModel.DataAnnotations;

namespace Bmerketo.Models.ViewModels.Account
{
    public class UpdateUserProfileVM
    {
        public Guid Id { get; set; }
        
        [Required]
        [Display(Name = "First Name*")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Last Name*")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Street Name*")]
        public string StreetName { get; set; } = null!;

        [Display(Name = "Postal Code*")]
        public string PostalCode { get; set; } = null!;

        [Display(Name = "City*")]
        public string City { get; set; } = null!;

        [Display(Name = "Mobile (optinal)")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Company (optinal)")]
        public string? Company { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail Address*")]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password*")]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password*")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;

        [Display(Name = "Upload Profile Image (optional)")]
        public string? Image { get; set; }
        public string? ReturnUrl { get; set; }
        [Display(Name = "Upload Profile Image (optional)")]
        public IFormFile? ProfileImage { get; set; }


        //[Required(ErrorMessage = "Du måste acceptera användarvillkoren för att fortsätta")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Du måste acceptera användarvillkoren för att fortsätta")]
        [Display(Name = "I have read and accepts the terms and agreements")]
        public bool? TermsAndAggreements { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
