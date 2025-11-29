using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using Online_Medical.Interface;
using Online_Medical.Models;
using Online_Medical.Repository;
using Online_Medical.ViewModel;
using System.Security.Policy;

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


        #region old GetCreateDoctorViewModelAsync()
        //public async Task<DoctorRegisterViewModel> GetCreateDoctorViewModelAsync()
        //{
        //    var specializations = await _specializationRepository.GetAllAsync();

        //    var vm = new DoctorRegisterViewModel
        //    {
        //        SpecializationNames = specializations
        //            .Select(s => new SelectListItem
        //            {
        //                Text = s.Name,
        //                Value = s.Id.ToString()
        //            })
        //            .ToList(),

        //        GenderOptions = GenderList.GetEnumSelectList<Gender>()
        //    };

        //    return vm;
        //}
        #endregion




        //public async Task RegisterDoctor(DoctorRegisterViewModel doctorVM)
        //{
        //    // 1. Map ViewModel to Identity User and create the user
        //    ApplicationUser user = _mapper.Map<ApplicationUser>(doctorVM);
        //    var result = await _userManager.CreateAsync(user, doctorVM.Password);

        //    // Check if Identity User creation failed
        //    if (!result.Succeeded)
        //    {
        //        // No need to delete the user here, as creation itself failed.
        //        // We throw an exception with the Identity errors.
        //        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        //        throw new Exception($"Failed to create Identity user: {errors}");
        //    }

        //    // 2. Map ViewModel to Doctor entity and link it to the user
        //    Doctor doctor = _mapper.Map<Doctor>(doctorVM);
        //    doctor.Id = user.Id;         // set primary key manually
        //    doctor.ApplicationUser = user;
        //    // 3. Attempt to save the Doctor record to the database
        //    try
        //    {
        //        _doctorRepository.Add(doctor);
        //        await _doctorRepository.SaveAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        // CRITICAL ROLLBACK: If saving the Doctor fails, delete the previously created Identity User
        //        await _userManager.DeleteAsync(user);

        //        // Log the detailed exception (e.g., database constraint violation)
        //        // You should use a real logging service here (e.g., ILogger)
        //        System.Console.WriteLine($"Database Save Error for Doctor ID {user.Id}: {ex.Message}");

        //        // Throw a new, clean exception to the calling layer
        //        throw new Exception("Failed to save doctor details to the database (User creation rolled back).", ex);
        //    }
        //}




        public async Task<List<DoctorIndexViewModel>> GetAllDoctorsAsync()
        {
            var doctors = await _doctorRepository.GetAllAsync();
            var doctorVMs = doctors
                .Select((d, index) => new DoctorIndexViewModel
                {
                    Id = d.Id,
                    SerialNumber = index + 1,
                    FullName = $"{d.ApplicationUser?.FirstName ?? ""} {d.ApplicationUser?.LastName ?? ""}".Trim(),
                    Email = d.ApplicationUser?.Email ?? "N/A",
                    PhoneNumber = d.ApplicationUser?.PhoneNumber ?? "N/A",
                    Gender = d.ApplicationUser?.Gender,
                    specializationName = d.Specialization?.Name ?? "Not Assigned"
                })
                .ToList();
            return doctorVMs;
        }

        public async Task<DoctorUpdateViewModel>GetDoctorByIdAsync(string id)
        {
            var vm = new DoctorUpdateViewModel();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return null; 
            }
            _mapper.Map(user, vm);
            vm.Id = user.Id;
            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor == null)
            {
                return null;
            }
            _mapper.Map(doctor, vm);
            
            return vm;
        }


        public async Task<IdentityResult> RegisterDoctor(DoctorAddViewModel vm)
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(vm);
            user.UserName = vm.Email;

            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }
            
            Doctor doctor = new Doctor
            {
                Id = user.Id,
                ApplicationUser = user
            };

            try
            {
                await _doctorRepository.AddAsync(doctor);

                await _doctorRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                var innerError = ex.InnerException != null ? ex.InnerException.Message : "No inner exception.";

                throw new Exception($"Failed to save Doctor entity. Details: {innerError}", ex);
            }

            return result;
        }
        public async Task<bool> UpadateDoctorAsync(DoctorUpdateViewModel _Vm) {
            ApplicationUser user = await _userManager.FindByIdAsync(_Vm.Id);

            if (user == null)
            {
                return false; // User not found
            }
            _mapper.Map(_Vm, user);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return false;
            }

            
            var doctor =await _doctorRepository.GetByIdAsync(_Vm.Id);
            if(doctor == null)
            {
                return false; // Doctor not found
            }   
            _mapper.Map(_Vm, doctor);
            try
            {
                _doctorRepository.UpdateAsync(doctor);
                return true;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Database Save Error for Doctor ID {user.Id}: {ex.Message}");
                throw new Exception("Failed to save doctor details to the database.", ex);
            }
          

        }


    }
}

