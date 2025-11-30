using Microsoft.AspNetCore.Identity;
using Online_Medical.ViewModel;
using Online_Medical.Models;

namespace Online_Medical.Interface
{
    public interface IAdminService
    {
        Task<List<AdminPatientViewModel>> GetAdminPatientListAsync();

     
       
     
        Task<IdentityResult> DeletePatientAsync(string userId);
      
        Task<IdentityResult> CreateClinicAsync(ClinicCreateViewModel model);
        Task<ClinicListViewModel> GetClinicListAsync();
        Task<ClinicDetailsViewModel?> GetClinicDetailsAsync(int clinicId);
        Task<ClinicEditViewModel?> GetClinicForEditAsync(int id);
        Task<IdentityResult> UpdateClinicAsync(ClinicEditViewModel model);
        Task<IdentityResult> DeleteClinicAsync(int clinicId);
    }

}
