using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password is Required")]
        [MinLength(5, ErrorMessage = "Min Length Is 5 Chars")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is Required")]
        [Compare("Password", ErrorMessage = "Confirm Password does't Match")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
