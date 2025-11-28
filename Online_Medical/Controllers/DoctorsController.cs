using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Medical.ALL_DATA;
using Online_Medical.Models;
using Online_Medical.Services;
using Online_Medical.ViewModel;
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
        public async Task<ActionResult> Index()
        {

            var doctorList = await _service.GetAllDoctorsAsync();

            return  View("Index", doctorList);
        }


        // GET: DoctorsController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var vm=await _service.GetDoctorByIdAsync(id);
            return View("Details",vm);
        }

        // GET: DoctorsController/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {   
            //DoctorRegisterViewModel _vm = await _service.GetCreateDoctorViewModelAsync();
            //_vm.GenderOptions = GenderList.GetEnumSelectList<Gender>();

            return View("Create");
        }

        // POST: DoctorsController/Create
        //[HttpPost]
        //public async Task<ActionResult> Create()
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //    try
        //    //    {
        //    //        await _service.RegisterDoctor(vm); 
        //    //        return RedirectToAction("Index");
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        // Optionally add the exception message to ModelState
        //    //        ModelState.AddModelError("", ex.Message);
        //    //    }
        //    //}
        //    //DoctorRegisterViewModel _vm = await _service.GetCreateDoctorViewModelAsync();
        //    return View("Create");
        //}


        // GET: DoctorsController/Edit/5

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            //GetDoctorByIdAsync have the same content as Edit (same viewmodel)
            DoctorUpdateViewModel vm =await _service.GetDoctorByIdAsync(id);
            vm.GenderOptions =  GenderList.GetEnumSelectList< Gender>();
            vm.SpecializationNames = await _context.Specializations
                .Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToListAsync();
            return View("Edit", vm);

        }


        // POST: DoctorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task <ActionResult> Edit(DoctorUpdateViewModel vm)
        {
            try
            {
                await _service.UpadateDoctorAsync(vm);
                return RedirectToAction("Details",vm);
            }
            catch
            {
                vm.GenderOptions = GenderList.GetEnumSelectList<Gender>();
                vm.SpecializationNames = await _context.Specializations
                    .Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Text = s.Name,
                        Value = s.Id.ToString()
                    }).ToListAsync();
                return View("Edit",vm);
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
