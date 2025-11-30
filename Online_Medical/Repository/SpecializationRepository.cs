using Microsoft.EntityFrameworkCore;
using Online_Medical.ALL_DATA;

namespace Online_Medical.Repository
{
    public class SpecializationRepository : IRepository<Specialization, int>
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

        public async Task AddAsync(Specialization obj)
        {
            await _context.Specializations.AddAsync(obj);
        }


        //Not implemented async methods below
        public void Delete(int id)
        {
            var specializationToRemove = GetById(id);
            if (specializationToRemove != null)
            {
                _context.Specializations.Remove(specializationToRemove);
            }

        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        //-----------------------------------

        public IEnumerable<Specialization> GetAll()
        {
            return _context.Specializations.ToList();
        }
        public async Task<IEnumerable<Specialization>> GetAllAsync()
        {
            return await _context.Specializations
                .Include(d=>d.Doctors)
                    .ThenInclude(d=>d.ApplicationUser)
                .ToListAsync();
        }


        public Specialization GetById(int id)
        {
            return _context.Specializations.FirstOrDefault(s => s.Id == id);
        }

        public async Task<Specialization> GetByIdAsync(int id)
        {
            return await _context.Specializations.FirstOrDefaultAsync(s => s.Id == id);
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
            _context.SaveChanges();
        }

        public async Task UpdateAsync(Specialization obj)
        {
            _context.Specializations.Update(obj);
            await _context.SaveChangesAsync();
        }
    }
}
