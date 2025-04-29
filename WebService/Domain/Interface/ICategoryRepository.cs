using WebService.Domain.Entities;

namespace WebService.Domain
{
    namespace Interface
    {
        public interface ICategoryRepository : IBaseRepository<Category>
        {
            Task<List<Category>> ListAlByNamesync(String name);
            
        }
    }
}
