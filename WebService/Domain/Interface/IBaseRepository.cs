namespace WebService.Domain
{
    namespace Interface
    {
        public interface IBaseRepository<T>
        {
            Task<T> AddAsync(T obj);
            Task<bool> UpdateAsync(T obj);
            Task<bool> DeleteAsync(T obj);
            Task<List<T>> GetPagedAsync(int page, int pageSize);
            Task<T?> GetByIdAsync(Guid id);
            Task<T?> GetByIdWithDetailsAsync(Guid id);
        }
    }
}
