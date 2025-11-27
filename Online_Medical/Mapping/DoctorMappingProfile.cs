using AutoMapper;
using Online_Medical.Models;
using Online_Medical.ViewModels;

namespace Online_Medical.Mapping
{
    public class DoctorMappingProfile:Profile
    {

            public DoctorMappingProfile()
            {
                //CreateMap<Source, Destination>();
                CreateMap<DoctorRegisterViewModel, ApplicationUser>()
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)) // Username = Email
                    .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Identity handles password hashing
                    .ForMember(dest => dest.Id, opt => opt.Ignore()); // Identity will generate Id

                // Map for Doctor
                CreateMap<DoctorRegisterViewModel, Doctor>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore()) // Will be filled with user.Id after identity creation
                    .ForMember(dest => dest.Specialization, opt => opt.Ignore()) // navigation ignored
                    .ForMember(dest => dest.ApplicationUser, opt => opt.Ignore()); // navigation ignored


        }
    }
}
