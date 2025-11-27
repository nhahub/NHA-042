using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Medical.ALL_DATA;
using Online_Medical.Models;
using Online_Medical.Services;
using Online_Medical.ViewModels;
using System.Threading.Tasks;

namespace Online_Medical.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly DoctorService _service;
        private readonly AppDbContext _context;

        public DoctorsController(DoctorService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        // GET: DoctorsController
        public ActionResult Index()
        {

            var doctorList = _context.Doctors.ToList(); 

            return View("Index", doctorList);
        }


        // GET: DoctorsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DoctorsController/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {   
            DoctorRegisterViewModel _vm = await _service.GetCreateDoctorViewModelAsync();
            _vm.GenderOptions = GenderList.GetEnumSelectList<Gender>();

            return View("Create", _vm);
        }

        // POST: DoctorsController/Create
        [HttpPost]
        public async Task<ActionResult> Create(DoctorRegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.RegisterDoctor(vm); // ✅ Await the async method
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Optionally add the exception message to ModelState
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View("Create", vm);
        }


        // GET: DoctorsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DoctorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DoctorsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DoctorsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
