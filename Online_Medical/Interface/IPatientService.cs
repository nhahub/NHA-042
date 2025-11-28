using Microsoft.AspNetCore.Identity;
using Online_Medical.ViewModel;

namespace Online_Medical.Interface
{
    public interface IPatientService
    {
        Task<Patient?> GetPatientProfileDetailsAsync(string userId);
        Task<PatientProfileViewModel?> GetPatientProfileForEditAsync(string userId);
        Task<(IdentityResult identityResult, IEnumerable<IdentityError> passwordErrors, string? newImageUrl)>
            UpdatePatientProfileAsync(PatientProfileViewModel model, string wwwRootPath);
    }
}

