using AutoMapper;

namespace Online_Medical.Mapping
{
    public class SpecializationMappingProfile:Profile
    {
        public SpecializationMappingProfile()
        {
            // Map for Specialization
            CreateMap<Specialization, specializationGetAllViewModel>();

               CreateMap<specializationGetAllViewModel, Specialization>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());



        }
    }
}
