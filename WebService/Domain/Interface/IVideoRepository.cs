using WebService.Domain.Entities;

namespace WebService.Domain
{
    namespace Interface
    {
        public interface IVideoRepository : IBaseRepository<Video>
        {
            Task<List<Video>> ListByCategoryAsync(Guid categoryId, int page, int pageSize);
            Task<List<Video>> ListBySerieAsync(Guid serieId, int page, int pageSize);
        }
    }
}
