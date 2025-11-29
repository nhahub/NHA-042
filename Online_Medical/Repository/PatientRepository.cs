using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_Medical.ALL_DATA;

using Online_Medical.Models;
using Online_Medical.Interface;
namespace Online_Medical.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;

        public PatientRepository(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // تنفيذ دوال IRepository<Patient, string>
        public void Add(Patient obj) => _context.Set<Patient>().Add(obj);
        public void Update(Patient obj) => _context.Set<Patient>().Update(obj);
        public void Delete(string id) { var patient = GetById(id); if (patient != null) _context.Set<Patient>().Remove(patient); }
        public Patient GetById(string id) => _context.Set<Patient>().Find(id);
        public IEnumerable<Patient> GetAll() => _context.Set<Patient>().ToList();
        public async Task<IEnumerable<Patient>> GetAllAsync() => await _context.Set<Patient>().ToListAsync();
        public void Save() => _context.SaveChanges();
        public Task SaveAsync() => _context.SaveChangesAsync();

        // تنفيذ الدوال الخاصة بـIPatientRepository
        public Task<Patient?> GetPatientWithUserByIdAsync(string patientId)
        {
            return _context.Patients.Include(p => p.ApplicationUser).SingleOrDefaultAsync(p => p.Id == patientId);
        }
        public Task<ApplicationUser?> FindUserByIdAsync(string userId) => _userManager.FindByIdAsync(userId);
        public Task<IdentityResult> UpdateApplicationUserAsync(ApplicationUser user) => _userManager.UpdateAsync(user);
        public Task<bool> CheckPasswordAsync(ApplicationUser user, string password) => _userManager.CheckPasswordAsync(user, password);
        public Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword) => _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        public Task UpdateSecurityStampAsync(ApplicationUser user) => _userManager.UpdateSecurityStampAsync(user);
    
    // في PatientRepository.cs، أضف هذه الدوال الأربعة:

// 1. تنفيذ AddAsync
public Task AddAsync(Patient obj)
        {
            // نستخدم Add العادية لأن الحفظ (SaveAsync) يتم لاحقاً
            _context.Set<Patient>().Add(obj);
            return Task.CompletedTask;
        }

        // 2. تنفيذ UpdateAsync
        public Task UpdateAsync(Patient obj)
        {
            _context.Set<Patient>().Update(obj);
            return Task.CompletedTask;
        }

        // 3. تنفيذ GetByIdAsync
        public async Task<Patient> GetByIdAsync(string id)
        {
            // نستخدم FindAsync لجلب البيانات بشكل غير متزامن
            return await _context.Set<Patient>().FindAsync(id);
        }

        // 4. تنفيذ DeleteAsync
        public async Task DeleteAsync(string id)
        {
            // نستخدم GetByIdAsync لضمان أن العملية غير متزامنة بالكامل
            var patient = await GetByIdAsync(id);
            if (patient != null)
            {
                _context.Set<Patient>().Remove(patient);
            }
        }
    }
}
