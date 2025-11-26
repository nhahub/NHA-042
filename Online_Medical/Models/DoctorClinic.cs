using System.ComponentModel.DataAnnotations;

namespace Online_Medical.Models
{
    public class DoctorClinic
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [Required]
        public string Day { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }
    }
}
    

