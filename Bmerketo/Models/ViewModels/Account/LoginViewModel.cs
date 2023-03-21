using Bmerketo.Models.Form;
using System.ComponentModel.DataAnnotations;

namespace Bmerketo.Models.ViewModels.Account
{
    public class LoginViewModel
    {
        public string ReturnUrl { get; set; } = null!;

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;
        public SignInForm Form { get; set; } = new SignInForm();
    }
}
