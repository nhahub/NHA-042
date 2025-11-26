using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Medical.ALL_DATA;
using Online_Medical.Models;

namespace Online_Medical.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _context;

        public AppointmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Appointments.Include(a => a.Clinic).Include(a => a.Doctor).Include(a => a.Patient);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Clinic)
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["ClinicId"] = new SelectList(_context.Clinics, "Id", "Name");
            var doctors = _context.Doctors.Select(d => new {
                Id = d.Id,
                FullName = d.FirstName + " " + d.LastName
            });
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "FullName");
            var patients = _context.Patients.Select(p => new {
                Id = p.Id,
                FullName = p.FirstName + " " + p.LastName
            });
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FullName");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AppointmentDate,Status,PaymentStatus,DoctorId,PatientId,ClinicId")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClinicId"] = new SelectList(_context.Clinics, "Id", "City", appointment.ClinicId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Email", appointment.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Email", appointment.PatientId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["ClinicId"] = new SelectList(_context.Clinics, "Id", "Name", appointment.ClinicId);
            var doctors = _context.Doctors.Select(d => new {
                Id = d.Id,
                FullName = d.FirstName + " " + d.LastName
            });
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "FullName", appointment.DoctorId);
            var patients = _context.Patients.Select(p => new {
                Id = p.Id,
                FullName = p.FirstName + " " + p.LastName
            });
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FullName", appointment.PatientId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AppointmentDate,Status,PaymentStatus,DoctorId,PatientId,ClinicId")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClinicId"] = new SelectList(_context.Clinics, "Id", "City", appointment.ClinicId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Email", appointment.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Email", appointment.PatientId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Clinic)
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }
    }
}
