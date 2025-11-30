using Microsoft.AspNetCore.Identity;
using Online_Medical.Models; // نحتاج هذا لتعريف Patient و ApplicationUser
using Online_Medical.Interface; // نحتاج هذا لتعريف IRepository

namespace Online_Medical.Interface
{
    // هذا هو التعريف الصحيح: واجهة واحدة ترث وتضيف الدوال المطلوبة
    public interface IPatientRepository : IRepository<Patient,string>
    {
        // العمليات الخاصة التي تحتاج Identity أو Include:
        Task<Patient?> GetPatientWithUserByIdAsync(string patientId);
        Task<ApplicationUser?> FindUserByIdAsync(string userId);

        Task<IdentityResult> UpdateApplicationUserAsync(ApplicationUser user);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword);
        Task UpdateSecurityStampAsync(ApplicationUser user);
    }
}
