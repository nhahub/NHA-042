//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Online_Medical.ALL_DATA;
//using Online_Medical.Models;
//using Online_Medical.ViewModel;

//namespace Online_Medical.Controllers
//{
//    // [Authorize(Roles = "Admin")] // Uncomment when Admin role is fully implemented
//    public class AdminController : Controller
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly AppDbContext _context;

//        public AdminController(UserManager<ApplicationUser> userManager, AppDbContext context)
//        {
//            _userManager = userManager;
//            _context = context;
//        }

//        public async Task<IActionResult> Index()
//        {
//            // Fetch all users with the "Patient" role
//            var patients = await _userManager.GetUsersInRoleAsync("Patient");

//            var model = new List<AdminPatientViewModel>();

//            foreach (var user in patients)
//            {
//                // Get patient details to access appointments
//                var patientData = await _context.Patients
//                    .Include(p => p.Appointments)
//                    .FirstOrDefaultAsync(p => p.Id == user.Id);

//                model.Add(new AdminPatientViewModel
//                {
//                    Username = user.UserName,
//                    Email = user.Email,
//                    PhoneNumber = user.PhoneNumber,
//                    JoinDate = user.JoinDate,
//                    TotalAppointments = patientData?.Appointments?.Count ?? 0
//                });
//            }

//            return View(model);
//        }
//    }
//}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Medical.Interface;
using Online_Medical.ViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Medical.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService) // يعتمد فقط على الـService
        {
            _adminService = adminService;
        }

        // ============================================
        // 1. Index (جلب قائمة المرضى)
        // ============================================
        public async Task<IActionResult> Index()
        {
            var model = await _adminService.GetAdminPatientListAsync();
            return View(model);
        }

        // ============================================
        // 2. Details (توجيه لصفحة تفاصيل المريض)
        // ============================================
        [HttpGet]
        public IActionResult Details(string id)
        {
            // ينادي على PatientController: Details
            return RedirectToAction("Details", "Patient", new { id = id });
        }

        // ============================================
        // 3. Edit (توجيه لصفحة تعديل المريض)
        // ============================================
        [HttpGet]
        public IActionResult Edit(string id)
        {
            // ينادي على PatientController: Edit
            return RedirectToAction("Edit", "Patient", new { id = id });
        }

        // ============================================
        // 4. Delete (حذف المريض) - يتم استدعاؤها من الـJavaScript
        // ============================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _adminService.DeletePatientAsync(id);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "تم حذف المريض بنجاح.";
            }
            else
            {
                TempData["ErrorMessage"] = "حدث خطأ أثناء حذف المريض. " +
                                           string.Join(", ", result.Errors.Select(e => e.Description));
            }

            return RedirectToAction("Index");
        }
    }
}