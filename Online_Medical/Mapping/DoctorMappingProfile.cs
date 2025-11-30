using AutoMapper;
using Online_Medical.Models;
using Online_Medical.ViewModel;

namespace Online_Medical.Mapping
{
    public class DoctorMappingProfile:Profile
    {

            public DoctorMappingProfile()
            {

            // CreateMap<DoctorUpdateViewModel, ApplicationUser>()
            //.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)) // Username = Email
            //.ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Identity handles password hashing
            //.ForMember(dest => dest.Id, opt => opt.Ignore()); // Identity will generate Id
            //    

                CreateMap<DoctorAddViewModel, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.Ignore()) // Don't map UserName
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));


            //CreateMap<Source, Destination>();
            CreateMap<DoctorUpdateViewModel, ApplicationUser>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // Identity handles password hashing

                // Map for Doctor
                CreateMap<DoctorUpdateViewModel, Doctor>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Specialization, opt => opt.Ignore()) // navigation ignored
                    .ForMember(dest => dest.ApplicationUser, opt => opt.Ignore()); // navigation ignored

                CreateMap<ApplicationUser, DoctorUpdateViewModel>();

                CreateMap<Doctor, DoctorUpdateViewModel>();

                


        }
    }
}
