using System.ComponentModel.DataAnnotations;

namespace Online_Medical.Models
{
    public class Clinic
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Street { get; set; }
        public int BuildingNumber { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string City { get; set; }
        public string Phone { get; set; }
        // public string image { get; set; }
        public List<DoctorClinic> DoctorClinics { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
