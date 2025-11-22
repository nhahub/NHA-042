using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace Online_Medical.Models
{
    public enum Gender
    {
        Male,
        Female,
    }
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Phone), IsUnique = true)]
    public class Doctor
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone] 
        [MaxLength(20)]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }

        public Gender Gender { get; set; }
        [Range(0, 60)]
        public int ExperienceYears { get; set; }
        [MaxLength(500)]
        public string Bio { get; set; }

        public string? ProfileImage { get; set; }

        //[ForeignKey("Specialization")]
        public int? SpecializationId { get; set; }
        public Specialization? Specialization { get; set; }

        //public double Fees {get; set;}

        //Relation:
       
        public List<DoctorClinic> DoctorClinics { get; set; }
        public List<Doctor_WorkingHours> WorkingHours { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
