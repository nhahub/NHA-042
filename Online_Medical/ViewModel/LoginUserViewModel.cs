using System.ComponentModel.DataAnnotations;

namespace Online_Medical.ViewModel
{
    public class LoginUserViewModel
    {
        [Display(Name = "Username or Email")]
        [Required(ErrorMessage = "Please enter your username or email")]
        public string UserName { get; set; } 

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
