using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Medical.Models
{
    public class Patient
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string? ProfileImage { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
