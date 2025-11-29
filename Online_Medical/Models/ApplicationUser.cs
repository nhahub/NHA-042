using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Online_Medical.Models
{

    public class ApplicationUser:IdentityUser
    {

        [MaxLength(50)]
        public string? FirstName { get; set; }
        [MaxLength(50)]
        public string? LastName { get; set; }

        public Gender? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;



        // Navigation properties for one-to-one relationship
        public Patient PatientData { get; set; }
        public Doctor DoctorData { get; set; }
    }
}

