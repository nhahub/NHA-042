using System.ComponentModel.DataAnnotations;
namespace Online_Medical.Models
{
    public class Specialization
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}
