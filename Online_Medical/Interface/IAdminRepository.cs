using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Online_Medical.Models;
using Online_Medical.ViewModel;


namespace Online_Medical.Interface
{
    public interface IAdminRepository
    {
        Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName);

        // 2. جلب بيانات المريض مع المواعيد
        Task<Patient?> GetPatientDataWithAppointmentsAsync(string userId);

        // 3. حذف المستخدم بشكل كامل
        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);

        // 4. جلب المستخدم عن طريق الـID
        Task<ApplicationUser?> FindUserByIdAsync(string userId);
    }
}
