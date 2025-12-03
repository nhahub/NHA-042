using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Online_Medical.Controllers
{
    [Authorize(Roles ="Admin")]
    public class SpecializationController : Controller
    {
        private readonly SpecializationService _specializationService;

        public SpecializationController(SpecializationService specializationService)
        {
            _specializationService = specializationService;
        }


        // GET: SpecializationController

        public async Task<ActionResult> Index()
        {
            var SpecializationList = await _specializationService.GetAllSpecializations();


            return View("Index", SpecializationList);
        }

        // GET: SpecializationController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var specializationDetails = await _specializationService.GetSpecializationById(id);
            return View("Details",specializationDetails);
        }

        // GET: SpecializationController/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            specializationGetAllViewModel vm = new specializationGetAllViewModel();
            return View("Create", vm);
        }

        // POST: SpecializationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(specializationGetAllViewModel vm)
        {
            if (!ModelState.IsValid)
                return View("Create",vm);

            var success = await _specializationService.AddSpecialization(vm);
            if (success)
                return RedirectToAction("Index");
            
            else
            {
                ModelState.AddModelError("", "Something went wrong.Please try again.");
                return View("Create", vm);

            }
        }

        // GET: SpecializationController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var vm= await _specializationService.GetSpecializationById(id);
            return View("Edit",vm);
        }

        // POST: SpecializationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, specializationGetAllViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", vm);
            }
            var success = await _specializationService.UpdateSpecialization(vm);
            if (success)
                return RedirectToAction("Index");
            else
            {
                ModelState.AddModelError("", "Something went wrong.Please try again.");
                return View("Edit", vm);
            }

        }

        // GET: SpecializationController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View();
        }

        // POST: SpecializationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>  Delete(int id, IFormCollection collection)
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
