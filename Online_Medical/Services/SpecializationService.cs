using AutoMapper;
using Online_Medical.Repository;
using System.Threading.Tasks;

namespace Online_Medical.Services
{
    public class SpecializationService
    {
        private readonly IRepository<Specialization, int> _specializationRepository;
        private readonly IMapper _mapper;
        public SpecializationService(IRepository<Specialization, int> specializationRepository, IMapper mapper) {
            _specializationRepository = specializationRepository;
            _mapper = mapper;

        }

        public async Task<List<specializationGetAllViewModel>> GetAllSpecializations()
        {
            
            var specializations = await _specializationRepository.GetAllAsync();
            return _mapper.Map<List<specializationGetAllViewModel>>(specializations);
        }

        public async Task<bool> AddSpecialization(specializationGetAllViewModel vm) {
            try
            {
                var newSpecialization = _mapper.Map<Specialization>(vm);
                await _specializationRepository.AddAsync(newSpecialization);
                await _specializationRepository.SaveAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

        public async Task<specializationGetAllViewModel> GetSpecializationById(int id) { 
        
            var specialization = await _specializationRepository.GetByIdAsync(id);
            if (specialization == null)
                return null;

            var  vm= _mapper.Map<specializationGetAllViewModel>(specialization);
            return vm;
        }
        public async Task<bool> UpdateSpecialization(specializationGetAllViewModel vm)
        {
            var specializationToUpdate =await _specializationRepository.GetByIdAsync(vm.Id);
            if (specializationToUpdate == null)
                return false;
             _mapper.Map(vm, specializationToUpdate);
            try
            {
                await _specializationRepository.UpdateAsync(specializationToUpdate);
                await _specializationRepository.SaveAsync();
                return true;
            }
            catch (Exception)
            {
                return false;

            }

        }



    }
}
