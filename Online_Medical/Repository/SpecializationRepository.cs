using Microsoft.EntityFrameworkCore;
using Online_Medical.ALL_DATA;

namespace Online_Medical.Repository
{
    public class SpecializationRepository:IRepository<Specialization, int>
    {
        private readonly AppDbContext _context;
        public SpecializationRepository(AppDbContext context)
        {

            _context = context;
        }

        public void Add(Specialization obj)
        {
            _context.Specializations.Add(obj);
        }

        public void Delete(int id)
        {
            var specializationToRemove = GetById(id);
            if (specializationToRemove != null)
            {
                _context.Specializations.Remove(specializationToRemove);
            }

        }

        public IEnumerable<Specialization> GetAll()
        {
            return _context.Specializations.ToList();
        }
        public async Task<IEnumerable<Specialization>> GetAllAsync()
        {
            return await _context.Specializations.ToListAsync();
        }


        public Specialization GetById(int id)
        {
            return _context.Specializations.FirstOrDefault(s => s.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Specialization obj)
        {
            _context.Specializations.Update(obj);
        }
    }
}
