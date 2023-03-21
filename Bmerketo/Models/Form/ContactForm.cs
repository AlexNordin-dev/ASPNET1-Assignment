using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Bmerketo.Models.Form
{
    public class ContactForm
    {
        [Required]
        [Display(Name = "Name*")]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail Address*")]
        public string Email { get; set; } = null!;

        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Company (optinal)")]
        public string? Company { get; set; }



        [Required]
        [Display(Name = "Someting write*")]
        public string text { get; set; } = null!;
      



        [Required(ErrorMessage = "Du måste acceptera användarvillkoren för att fortsätta")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Du måste acceptera användarvillkoren för att fortsätta")]
        [Display(Name = "Save my name, email, and website in this browser for the next time I comment.")]
        public bool TermsAndAggreements { get; set; }
    }
}
