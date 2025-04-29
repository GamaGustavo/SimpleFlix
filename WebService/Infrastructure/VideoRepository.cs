using Microsoft.EntityFrameworkCore;
using WebService.Domain.Entities;
using WebService.Domain.Interface;
using WebService.Infrastructure.Data;

namespace WebService.Infrastructure
{
    namespace Repositories
    {
        public class VideoRepository(AppDbContext context) : IVideoRepository
        {
            public async Task<Video> AddAsync(Video obj)
            {
                await context.Videos.AddAsync(obj);
                await context.SaveChangesAsync();
                return obj;
            }
            public async Task<bool> UpdateAsync(Video obj)
            {
                 context.Videos.Update(obj);
                 return await context.SaveChangesAsync() > 0 ;
               
            }
            public async Task<bool> DeleteAsync(Video obj)
            {                
                 context.Videos.Remove(obj);
                 return await context.SaveChangesAsync() > 0;
            }
            public async Task<Video?> GetByIdAsync(Guid id)
            {
                return await context.Videos.FindAsync(id);
            }
            public async Task<Video?> GetByIdWithDetailsAsync(Guid id)
            {
                return await context.Videos
                     .Include(v => v.Category)         // Carrega a categoria
                     .Include(v => v.Series)           // Carrega a série (se aplicável)
                     .Include(v => v.Genres)           // Carrega os gêneros (relação N-N)
                     .ThenInclude(vg => vg.Genre)      // Carrega os detalhes do gênero
                     .FirstOrDefaultAsync(v => v.Id == id);
            }

            public async Task<List<Video>> GetPagedAsync(int page, int pageSize)
            {
                return await context.Videos
                    .Skip((page-1)*pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            public async Task<List<Video>> ListByCategoryAsync(Guid categoryId, int page, int pageSize)
            {
                return await context.Videos
                    .Where(v => v.CategoryId == categoryId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            public async Task<List<Video>> ListBySerieAsync(Guid serieId, int page, int pageSize)
            {
                return await context.Videos
                    .Where(v => v.SeriesId == serieId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
        }
    }
}
