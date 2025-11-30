using System.ComponentModel.DataAnnotations;

namespace Online_Medical.ViewModel
{
    public class ClinicEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Clinic Name is required")]
        [Display(Name = "Clinic Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Street is required")]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Display(Name = "Building Number")]
        public int? BuildingNumber { get; set; }

        [Required(ErrorMessage = "Region is required")]
        [Display(Name = "Region")]
        public string Region { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }
    }
}
