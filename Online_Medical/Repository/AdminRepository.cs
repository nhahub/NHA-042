using Microsoft.AspNetCore.Identity;
using Online_Medical.ALL_DATA;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Online_Medical.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;

        public AdminRepository(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName)
        {
            // تستخدم دالة الـIdentity المباشرة
            return _userManager.GetUsersInRoleAsync(roleName);
        }

        public Task<Patient?> GetPatientDataWithAppointmentsAsync(string userId)
        {
            // تستخدم الـAppDbContext لعمل Include على جدول Appointments
            return _context.Patients
                .Include(p => p.Appointments)
                .FirstOrDefaultAsync(p => p.Id == userId);
        }

        public Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            // تستخدم دالة الـIdentity المباشرة لحذف المستخدم
            return _userManager.DeleteAsync(user);
        }

        public Task<ApplicationUser?> FindUserByIdAsync(string userId)
        {
            // تستخدم دالة الـIdentity المباشرة لجلب المستخدم
            return _userManager.FindByIdAsync(userId);
        }
    }
}
