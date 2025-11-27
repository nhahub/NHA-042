using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Online_Medical.Interface;
using Online_Medical.Models;
using Online_Medical.Repository;
using Online_Medical.ViewModels;

namespace Online_Medical.Services
{
    public class DoctorService
    {
        private readonly IRepository<Doctor, string> _doctorRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Specialization, int> _specializationRepository;

        public DoctorService(IRepository<Doctor, string> doctorRepository,
            IMapper mapper, UserManager<ApplicationUser> userManager,
            IRepository<Specialization, int> specializationRepoistor)
        {
            this._doctorRepository = doctorRepository;
            _mapper = mapper;
            _userManager = userManager;
            _specializationRepository = specializationRepoistor;
        }


        public async Task<DoctorRegisterViewModel> GetCreateDoctorViewModelAsync()
        {
            var specializations = await _specializationRepository.GetAllAsync();

            var vm = new DoctorRegisterViewModel
            {
                SpecializationNames = specializations
                    .Select(s => new SelectListItem
                    {
                        Text = s.Name,
                        Value = s.Id.ToString()
                    })
                    .ToList(),

                GenderOptions = GenderList.GetEnumSelectList<Gender>()
            };

            return vm;
        }


        public async Task RegisterDoctor(DoctorRegisterViewModel doctorVM)
        {
            // 1. Map ViewModel to Identity User and create the user
            ApplicationUser user = _mapper.Map<ApplicationUser>(doctorVM);
            var result = await _userManager.CreateAsync(user, doctorVM.Password);

            // Check if Identity User creation failed
            if (!result.Succeeded)
            {
                // No need to delete the user here, as creation itself failed.
                // We throw an exception with the Identity errors.
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to create Identity user: {errors}");
            }

            // 2. Map ViewModel to Doctor entity and link it to the user
            Doctor doctor = _mapper.Map<Doctor>(doctorVM);
            doctor.Id = user.Id;         // set primary key manually
            doctor.ApplicationUser = user;
            // 3. Attempt to save the Doctor record to the database
            try
            {
                _doctorRepository.Add(doctor);
                await _doctorRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                // CRITICAL ROLLBACK: If saving the Doctor fails, delete the previously created Identity User
                await _userManager.DeleteAsync(user);

                // Log the detailed exception (e.g., database constraint violation)
                // You should use a real logging service here (e.g., ILogger)
                System.Console.WriteLine($"Database Save Error for Doctor ID {user.Id}: {ex.Message}");

                // Throw a new, clean exception to the calling layer
                throw new Exception("Failed to save doctor details to the database (User creation rolled back).", ex);
            }
        }

    }
}
