using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Online_Medical.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Phone), IsUnique = true)]

    public class Patient
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
        public DateTime DateOfBirth { get; set; }
        public string? ProfileImage { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
