using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "inValid Email")]
        public string Email { get; set; }
    }
}
