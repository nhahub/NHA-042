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

       
        // 1. Index (جلب قائمة المرضى)
       
        public async Task<IActionResult> Index()
        {
            var model = await _adminService.GetAdminPatientListAsync();
            return View(model);
        }

        // 2. Details 
       
        [HttpGet]
        public IActionResult Details(string id)
        {
            // ينادي على PatientController: Details
            return RedirectToAction("Details", "Patient", new { id = id });
        }

       
        // 3. Edit
       
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

        // ============================================
        // 5. CreateClinic (GET) - عرض نموذج إنشاء العيادة
        // ============================================
        [HttpGet]
        public IActionResult CreateClinic()
        {
            return View(); // هذا يرسل النموذج الفارغ لملء البيانات
        }

        // ============================================
        // 6. CreateClinic (POST) - معالجة نموذج إنشاء العيادة
        // ============================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateClinic(ClinicCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // استدعاء الـService لتنفيذ المنطق المعقد
                var result = await _adminService.CreateClinicAsync(model);

                if (result.Succeeded)
                {
                    // النجاح: التوجيه لصفحة قائمة المشرف الرئيسية
                    TempData["SuccessMessage"] = "تم إنشاء العيادة وربط الأطباء بنجاح.";
                    return RedirectToAction("Index", "Admin");
                }

                // فشل: عرض رسائل الأخطاء من الـService (مثل عدم العثور على طبيب)
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // إذا فشل الـModelState أو فشل الـService، نعرض الـView مرة أخرى
            return View(model);
        }

        // ============================================
        // 7. ClinicIndex (GET)
        // ============================================
        [HttpGet]
        public async Task<IActionResult> ClinicIndex()
        {
            var model = await _adminService.GetClinicListAsync();
            return View(model);
        }

        // ============================================
        // 8. ClinicDetails (GET)
        // ============================================
        [HttpGet]
        public async Task<IActionResult> ClinicDetails(int id)
        {
            var model = await _adminService.GetClinicDetailsAsync(id);
            if (model == null)
            {
                TempData["ErrorMessage"] = $"لم يتم العثور على العيادة رقم {id}.";
                return RedirectToAction(nameof(ClinicIndex));
            }
            return View(model);
        }

        // ============================================
        // 9. ClinicDelete (POST)
        // ============================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClinicDelete(int id)
        {
            var result = await _adminService.DeleteClinicAsync(id);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = $"تم حذف العيادة بنجاح.";
            }
            else
            {
                TempData["ErrorMessage"] = "حدث خطأ أثناء حذف العيادة.";
            }
            return RedirectToAction(nameof(ClinicIndex));
        }

        // ============================================
        // 10. ClinicEdit (GET)
        // ============================================
        [HttpGet]
        public async Task<IActionResult> ClinicEdit(int id)
        {
            var model = await _adminService.GetClinicForEditAsync(id);
            if (model == null)
            {
                TempData["ErrorMessage"] = "العيادة غير موجودة.";
                return RedirectToAction(nameof(ClinicIndex));
            }
            return View(model);
        }

        // ============================================
        // 11. ClinicEdit (POST)
        // ============================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClinicEdit(ClinicEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.UpdateClinicAsync(model);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "تم تحديث بيانات العيادة بنجاح.";
                    return RedirectToAction(nameof(ClinicIndex));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

    }
}