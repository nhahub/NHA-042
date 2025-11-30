using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Online_Medical.Models;
using System.ComponentModel.DataAnnotations;

namespace Online_Medical.ViewModel
{
    
    public class DoctorUpdateViewModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "BirthDay")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public Gender? Gender { get; set; }

        [BindNever] // <-- Add this attribute
        public List<SelectListItem> GenderOptions { get; set; } = new List<SelectListItem>();

        [BindNever] // <-- Add this attribute
        public List<SelectListItem> SpecializationNames { get; set; } = new List<SelectListItem>();

        [Required]
        [Display(Name = "Specialization")]
        public int? SpecializationId { get; set; }

        [MaxLength(500)]
        public string? Bio { get; set; }

        [Range(0, 60)]
        [Display(Name = "Number of Experience Years")]
        public int ? ExperienceYears { get; set; }

        [Display(Name = "Profile Image")]
        public string? ProfileImage { get; set; }
    }
}
