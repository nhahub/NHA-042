using Online_Medical.ALL_DATA;

namespace Online_Medical.Interface
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        void Add(TEntity obj);
        void Update(TEntity obj);
        void Delete(TKey id); 
        TEntity GetById(TKey id); 
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();

        void Save();
        Task SaveAsync();
    }
}
