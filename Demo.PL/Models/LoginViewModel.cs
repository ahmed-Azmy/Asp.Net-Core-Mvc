using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "inValid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [MinLength(5, ErrorMessage = "Min Length Is 5 Chars")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
