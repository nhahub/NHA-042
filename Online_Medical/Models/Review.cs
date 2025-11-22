using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Online_Medical.Models
{
    public class Review
    {
      public int Id { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }= DateTime.Now;
        //Relations:
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
