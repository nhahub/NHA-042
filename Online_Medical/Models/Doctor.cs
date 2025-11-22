using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Medical.Models
{
    public enum Gender
    {
        Male,
        Female,
    }
    public class Doctor
    {

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public Gender Gender { get; set; }
        public int ExperienceYears { get; set; }

        public string Bio { get; set; }

        public string ProfileImage { get; set; }

        [ForeignKey("Specialization")]
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        //public double Fees {get; set;}

        //Relation:
    }
}
