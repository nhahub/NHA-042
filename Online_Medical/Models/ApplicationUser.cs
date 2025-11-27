using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Online_Medical.Models
{

    public class ApplicationUser:IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }



        // Navigation properties for one-to-one relationship
        public Patient PatientData { get; set; }
        public Doctor DoctorData { get; set; }
    }
}

