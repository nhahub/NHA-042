//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Online_Medical.ALL_DATA;
//using Online_Medical.Models;
//using Online_Medical.ViewModel;
//using System.IO;

//namespace Online_Medical.Controllers
//{
//    [Authorize]
//    public class PatientController : Controller
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly AppDbContext _context;
//        private readonly IWebHostEnvironment _hostEnvironment;

//        public PatientController(UserManager<ApplicationUser> userManager,
//                                 AppDbContext context,
//                                 IWebHostEnvironment hostEnvironment)
//        {
//            _userManager = userManager;
//            _context = context;
//            _hostEnvironment = hostEnvironment;
//        }

//        // ============================================
//        // 1. تفاصيل الحساب
//        // ============================================
//        public async Task<IActionResult> Details()
//        {
//            string userId = _userManager.GetUserId(User);

//            var patient = await _context.Patients
//                .Include(p => p.ApplicationUser)
//                .SingleOrDefaultAsync(p => p.Id == userId);

//            if (patient == null) return NotFound();

//            return View(patient);
//        }

//        // ============================================
//        // 2. Edit (GET)
//        // ============================================
//        [HttpGet]
//        public async Task<IActionResult> Edit()
//        {
//            string userId = _userManager.GetUserId(User);

//            var patient = await _context.Patients
//                .Include(p => p.ApplicationUser)
//                .SingleOrDefaultAsync(p => p.Id == userId);

//            if (patient == null) return NotFound();

//            var model = new PatientProfileViewModel
//            {
//                Id = patient.Id,
//                FirstName = patient.ApplicationUser.FirstName,
//                LastName = patient.ApplicationUser.LastName,
//                Email = patient.ApplicationUser.Email,
//                UserName = patient.ApplicationUser.UserName,
//                PhoneNumber = patient.ApplicationUser.PhoneNumber,
//                BirthDate = patient.ApplicationUser.DateOfBirth,
//                Gender = patient.ApplicationUser.Gender,
//                ExistingImageUrl = patient.ProfileImage ?? "/images/default.png"
//            };

//            return View(model);
//        }

//        // ============================================
//        // 3. Edit (POST) – تعديل البيانات + الصورة + الباسورد
//        // ============================================
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(PatientProfileViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                // 1. قراءة المستخدم
//                var user = await _userManager.FindByIdAsync(model.Id);
//                var patientProfile = await _context.Patients.SingleOrDefaultAsync(p => p.Id == model.Id);

//                if (user == null || patientProfile == null) return NotFound();

//                // ============================
//                // معالجة الصورة
//                // ============================
//                string wwwRootPath = _hostEnvironment.WebRootPath;
//                string newImageUrl = model.ExistingImageUrl;

//                if (model.ProfileImage != null)
//                {
//                    string fileName = Guid.NewGuid().ToString();
//                    var uploadsFolder = Path.Combine(wwwRootPath, "images", "PatientProfiles");
//                    var extension = Path.GetExtension(model.ProfileImage.FileName);

//                    if (!Directory.Exists(uploadsFolder))
//                        Directory.CreateDirectory(uploadsFolder);

//                    if (!string.IsNullOrEmpty(model.ExistingImageUrl) &&
//                        model.ExistingImageUrl != "/images/default.png")
//                    {
//                        var oldImagePath = Path.Combine(wwwRootPath, model.ExistingImageUrl.TrimStart('/'));
//                        if (System.IO.File.Exists(oldImagePath))
//                            System.IO.File.Delete(oldImagePath);
//                    }

//                    newImageUrl = "/images/PatientProfiles/" + fileName + extension;
//                    var filePath = Path.Combine(wwwRootPath, newImageUrl.TrimStart('/'));

//                    using (var fileStream = new FileStream(filePath, FileMode.Create))
//                    {
//                        await model.ProfileImage.CopyToAsync(fileStream);
//                    }
//                }

//                // ============================
//                // تحديث بيانات ApplicationUser
//                // ============================
//                user.FirstName = model.FirstName;
//                user.LastName = model.LastName;
//                user.UserName = model.UserName;
//                user.Email = model.Email;
//                user.PhoneNumber = model.PhoneNumber;
//                user.DateOfBirth = model.BirthDate;
//                user.Gender = model.Gender;

//                var identityResult = await _userManager.UpdateAsync(user);

//                if (!identityResult.Succeeded)
//                {
//                    foreach (var error in identityResult.Errors)
//                        ModelState.AddModelError("", error.Description);

//                    return View(model);
//                }

//                // ============================
//                // تغيير كلمة المرور (اختياري)
//                // ============================
//                if (!string.IsNullOrWhiteSpace(model.OldPassword) &&
//                    !string.IsNullOrWhiteSpace(model.NewPassword) &&
//                    !string.IsNullOrWhiteSpace(model.ConfirmPassword))
//                {
//                    if (model.NewPassword != model.ConfirmPassword)
//                    {
//                        ModelState.AddModelError("", "كلمة المرور الجديدة وتأكيدها غير متطابقين.");
//                        return View(model);
//                    }

//                    var checkOld = await _userManager.CheckPasswordAsync(user, model.OldPassword);
//                    if (!checkOld)
//                    {
//                        ModelState.AddModelError("", "كلمة المرور القديمة غير صحيحة.");
//                        return View(model);
//                    }

//                    var passwordResult = await _userManager.ChangePasswordAsync(
//                        user, model.OldPassword, model.NewPassword);

//                    if (!passwordResult.Succeeded)
//                    {
//                        foreach (var error in passwordResult.Errors)
//                            ModelState.AddModelError("", error.Description);

//                        return View(model);
//                    }
//                }

//                // ============================
//                // تحديث جدول Patient
//                // ============================
//                patientProfile.ProfileImage = newImageUrl;
//                _context.Patients.Update(patientProfile);
//                await _context.SaveChangesAsync();

//                // تحديث الـ Claims
//                await _userManager.UpdateSecurityStampAsync(user);

//                return RedirectToAction("Details");
//            }

//            return View(model);
//        }
//    }
//}
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
                    return RedirectToAction("Details");
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