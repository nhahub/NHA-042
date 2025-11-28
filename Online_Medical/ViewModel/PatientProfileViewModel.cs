using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Online_Medical.Models;
using System.ComponentModel.DataAnnotations;
namespace Online_Medical.ViewModel
{
    public class PatientProfileViewModel
    {
        public string Id { get; set; }

       
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required, Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        
        public string ExistingImageUrl { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }



        [Display(Name = "Change Profile Picture")]
        [ValidateNever]
        public IFormFile? ProfileImage { get; set; }

    }
}
