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
        public async Task DeletePatientProfileAsync(string userId)
        {
            var patient = await _context.Patients
                                        .FirstOrDefaultAsync(p => p.Id == userId);

            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<List<Patient>> GetAllPatientProfilesAsync()
        {
            return await _context.Patients
                                 .Include(p => p.ApplicationUser)
                                 .Include(p => p.Appointments)
                                 .ToListAsync();
        }

        //clinic
        public Task AddClinicAsync(Clinic clinic)
        {
            _context.Clinics.Add(clinic);
            return Task.CompletedTask;
        }
        public Task AddDoctorClinicAsync(DoctorClinic doctorClinic)
        {
            _context.DoctorClinics.Add(doctorClinic);
            return Task.CompletedTask;
        }
        public async Task<string?> FindDoctorIdByIdentifierAsync(string identifier)
        {
            // ... (هذه الدالة تبقى كما هي لأنها تستخدم await) ...
            var user = await _userManager.FindByNameAsync(identifier) ?? await _userManager.FindByEmailAsync(identifier);
            if (user == null)
                return null;
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == user.Id);
            return doctor?.Id;
        }

        public async Task SaveChangesAsync()
        {
            // 🎉 هذا هو المكان الذي يتم فيه تنفيذ أمر الحفظ الفعلي إلى SQL Server
            await _context.SaveChangesAsync();
        }

        public async Task<List<Clinic>> GetAllClinicsAsync()
        {
            // جلب العيادات مع تضمين جدول الربط لحساب عدد الأطباء
            return await _context.Clinics
                .Include(c => c.DoctorClinics)
                .ToListAsync();
        }

        public Task<Clinic?> GetClinicByIdAsync(int clinicId)
        {
            // جلب العيادة مع تفاصيل الأطباء للاستخدام في صفحة التفاصيل لاحقاً
            return _context.Clinics
                .Include(c => c.DoctorClinics)
                    .ThenInclude(dc => dc.Doctor)
                        .ThenInclude(d => d.ApplicationUser)
                .FirstOrDefaultAsync(c => c.Id == clinicId);
        }

        public Task UpdateClinicAsync(Clinic clinic)
        {
            _context.Clinics.Update(clinic);
            return Task.CompletedTask;
        }

        public async Task DeleteClinicAsync(int clinicId)
        {
            var clinic = await _context.Clinics.FindAsync(clinicId);
            if (clinic != null)
            {
                _context.Clinics.Remove(clinic);
                await _context.SaveChangesAsync();
            }
        }

    }
}

