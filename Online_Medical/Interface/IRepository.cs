using Online_Medical.ALL_DATA;

namespace Online_Medical.Interface
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        void Add(TEntity obj);
        Task AddAsync(TEntity obj);
        void Update(TEntity obj);
        Task UpdateAsync(TEntity obj);
        void Delete(TKey id); 
        Task DeleteAsync(TKey id);
        TEntity GetById(TKey id); 
        Task<TEntity> GetByIdAsync(TKey id);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();

        void Save();
        Task SaveAsync();
    }
}
