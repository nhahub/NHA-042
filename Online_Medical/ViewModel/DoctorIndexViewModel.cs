using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Online_Medical.ViewModel
{
    public class DoctorIndexViewModel
    {
        public  int SerialNumber { get; set; }

        public string Id { get; set; }
        public string FullName { get; set; }


        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }

        public string specializationName { get; set; }
        public int SpecializationId { get; set; }

        public string? ProfileImage { get; set; }

    }
}
