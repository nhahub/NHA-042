using Microsoft.AspNetCore.Identity;
using Online_Medical.ViewModel;

namespace Online_Medical.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        // يتم حقن (Injection) الـRepository هنا
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        // 1. منطق جلب قائمة المرضى (منطق Index)
        public async Task<List<AdminPatientViewModel>> GetAdminPatientListAsync()
        {
            // استدعاء من الـRepository لجلب مستخدمي دور "Patient"
            var patients = await _adminRepository.GetUsersInRoleAsync("Patient");

            var model = new List<AdminPatientViewModel>();

            foreach (var user in patients)
            {
                // استدعاء من الـRepository لجلب بيانات المريض والمواعيد المرتبطة
                var patientData = await _adminRepository.GetPatientDataWithAppointmentsAsync(user.Id);

                model.Add(new AdminPatientViewModel
                {
                    Id = user.Id, // مهم جداً للتعديل والحذف
                    Username = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    JoinDate = user.JoinDate,
                    TotalAppointments = patientData?.Appointments?.Count ?? 0
                });
            }
            return model;
        }

        // 2. منطق حذف المريض
        public async Task<IdentityResult> DeletePatientAsync(string userId)
        {
            // 1. جلب المستخدم من الـRepository
            var user = await _adminRepository.FindUserByIdAsync(userId);

            if (user == null)
            {
                // إذا لم يتم العثور عليه، نعتبر العملية ناجحة تقنياً لمنع ظهور خطأ غير ضروري
                return IdentityResult.Success;
            }

            // 2. حذف المستخدم من الـRepository (وهو يستخدم الـUserManager)
            return await _adminRepository.DeleteUserAsync(user);
        }
    }
}
