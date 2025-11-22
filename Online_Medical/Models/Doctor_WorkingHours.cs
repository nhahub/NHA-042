using System.ComponentModel.DataAnnotations;

namespace Online_Medical.Models
{
    public class Doctor_WorkingHours
    {
        public int Id { get; set; }

        [Required]
        public string Day { get; set; } 

        public string StartTime { get; set; } 
        public string EndTime { get; set; }   

        
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
