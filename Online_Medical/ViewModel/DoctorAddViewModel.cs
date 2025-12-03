using System.ComponentModel.DataAnnotations;

namespace Online_Medical.ViewModel
{
    public class DoctorAddViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]

        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }


    }
}
