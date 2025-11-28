using Microsoft.AspNetCore.Identity;
using Online_Medical.ViewModel;

namespace Online_Medical.Interface
{
    public interface IAdminService
    {// 1. جلب قائمة المرضى المعروضة في Index
        Task<List<AdminPatientViewModel>> GetAdminPatientListAsync();

        // 2. حذف المريض
        Task<IdentityResult> DeletePatientAsync(string userId);
    }
}
