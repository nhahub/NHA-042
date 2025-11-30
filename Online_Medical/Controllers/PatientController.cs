
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Online_Medical.ViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Medical.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientController(IPatientService patientService,
                                 IWebHostEnvironment hostEnvironment,
                                 UserManager<ApplicationUser> userManager)
        {
            _patientService = patientService;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
        }

        // ============================================
        // 1. Details (يقبل ID المشرف أو يستخدم ID المستخدم الحالي)
        // ============================================
        public async Task<IActionResult> Details(string? id)
        {
            // يُستخدم الـID الممرر من الـAdmin أو الـID الخاص بالمستخدم الحالي
            string userId = id ?? _userManager.GetUserId(User);

            var patient = await _patientService.GetPatientProfileDetailsAsync(userId);

            if (patient == null) return NotFound();

            return View(patient);
        }

        // ============================================
        // 2. Edit (GET)
        // ============================================
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            string userId = id ?? _userManager.GetUserId(User);

            var model = await _patientService.GetPatientProfileForEditAsync(userId);

            if (model == null) return NotFound();

            return View(model);
        }

        // ============================================
        // 3. Edit (POST)
        // ============================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PatientProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                // كل منطق التحديث تم نقله إلى الـService
                var (identityResult, passwordErrors, newImageUrl) =
                    await _patientService.UpdatePatientProfileAsync(model, _hostEnvironment.WebRootPath);

                if (identityResult.Succeeded && !passwordErrors.Any())
                {
                    return RedirectToAction("Details", new { id = model.Id });
                }

                // إضافة الأخطاء إلى الـModelState وعرض الـView مرة أخرى
                foreach (var error in identityResult.Errors.Concat(passwordErrors))
                    ModelState.AddModelError("", error.Description);

                model.ExistingImageUrl = newImageUrl ?? model.ExistingImageUrl;
                return View(model);
            }

            return View(model);
        }
    }
}