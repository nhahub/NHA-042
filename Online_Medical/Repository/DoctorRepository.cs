using Microsoft.EntityFrameworkCore;
using Online_Medical.ALL_DATA;
using Online_Medical.Interface;
using Online_Medical.Models;

namespace Online_Medical.Repository
{
    public class DoctorRepository : IRepository<Doctor, string>
    {
        //crud


        AppDbContext _context;
        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Doctor obj)
        {
            _context.Doctors.Add(obj);
        }

        public void Delete(string id)
        {
            
            var doctorToRemove = GetById(id);
            if (doctorToRemove != null)
            {
                _context.Doctors.Remove(doctorToRemove);
            }
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _context.Doctors.ToList();
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public Doctor GetById(string id)
        {
            return _context.Doctors.FirstOrDefault(d=>d.Id==id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Update(Doctor obj)
        {
            _context.Doctors.Update(obj);
        }
    }
}
