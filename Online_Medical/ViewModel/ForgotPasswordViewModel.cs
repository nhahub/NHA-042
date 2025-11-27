
using System.ComponentModel.DataAnnotations;
namespace Online_Medical.ViewModel

{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
