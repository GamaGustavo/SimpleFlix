using Microsoft.EntityFrameworkCore;
using WebService.Domain.Entities;
using WebService.Domain.Interface;
using WebService.Infrastructure.Data;

namespace WebService.Infrastructure
{
    namespace Repositories
    {
        public class GenreRepository(AppDbContext context) : IGenreRepository
        {
            public async Task<Genre> AddAsync(Genre obj)
            {
                await context.Genres.AddAsync(obj);
                await context.SaveChangesAsync();
                return obj;
            }

            public async Task<bool> DeleteAsync(Genre obj)
            {
                context.Genres.Remove(obj);
                return await context.SaveChangesAsync() > 0;
            }

            public async Task<Genre?> GetByIdAsync(Guid id)
            {
                return await context.Genres.FindAsync(id);
            }

            public async Task<Genre?> GetByIdWithDetailsAsync(Guid id)
            {
                return await context.Genres
                    .Include(g => g.Videos)
                    .FirstOrDefaultAsync(g => g.Id == id);
            }

            public async Task<List<Genre>> GetPagedAsync(int page, int pageSize)
            {
                return await context.Genres
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            public async Task<bool> UpdateAsync(Genre obj)
            {
                context.Genres.Update(obj);
                return await context.SaveChangesAsync() > 0;
                 
            }
        }
    }
}
