using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Online_Medical.Models;
using Online_Medical.ViewModel;


namespace Online_Medical.Interface
{
    public interface IAdminRepository
    {
       

        
        Task<Patient?> GetPatientDataWithAppointmentsAsync(string userId);

       
        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);

        Task<ApplicationUser?> FindUserByIdAsync(string userId);
        Task DeletePatientProfileAsync(string userId);
        Task<List<Patient>> GetAllPatientProfilesAsync();
        //clinic
        Task AddClinicAsync(Clinic clinic);
        Task AddDoctorClinicAsync(DoctorClinic doctorClinic);
        Task<string?> FindDoctorIdByIdentifierAsync(string identifier);
        Task SaveChangesAsync();
        Task<List<Clinic>> GetAllClinicsAsync();
        Task<Clinic?> GetClinicByIdAsync(int clinicId);
        Task UpdateClinicAsync(Clinic clinic);
        Task DeleteClinicAsync(int clinicId);

    }
}
