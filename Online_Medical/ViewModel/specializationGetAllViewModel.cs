using System.ComponentModel.DataAnnotations;

namespace Online_Medical.ViewModel
{
    public class specializationGetAllViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Specialization Name")]
        public string Name { get; set; }


        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public  List<Doctor>? Doctors { get; set; }    

    }
}
