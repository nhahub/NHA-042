using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace Online_Medical.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Admin
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
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
