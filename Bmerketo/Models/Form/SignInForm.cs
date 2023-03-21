using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Bmerketo.Models.Form
{
    public class SignInForm
    {
        [Required]
        [Display(Name = "E-Mail Address*")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [Display(Name = "Password*")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Please keep me logged in")]
        public bool RememberMe { get; set; } = false;

        public string ReturnUrl { get; set; } = "/";
    }
}
