using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace Online_Medical.Models
{

    public class Doctor
    {

        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }



        [Range(0, 60)]
        public int ExperienceYears { get; set; }



        [MaxLength(500)]
        public string? Bio { get; set; }

        public string? ProfileImage { get; set; }

        //[ForeignKey("Specialization")]
        public int? SpecializationId { get; set; }
        public Specialization? Specialization { get; set; }


        //Relation:
       
        public List<DoctorClinic> DoctorClinics { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
