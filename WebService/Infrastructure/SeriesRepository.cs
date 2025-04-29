using Microsoft.EntityFrameworkCore;
using WebService.Domain.Entities;
using WebService.Domain.Interface;
using WebService.Infrastructure.Data;

namespace WebService.Infrastructure
{
    namespace Repositories
    {
        public class SeriesRepository(AppDbContext context) : ISeriesRepository
        {
            public async Task<Series> AddAsync(Series obj)
            {
                await context.Series.AddAsync(obj);
                await context.SaveChangesAsync();
                return obj;
            }

            public async Task<bool> DeleteAsync(Series obj)
            {
                context.Series.Remove(obj);
                return await context.SaveChangesAsync() > 0;
            }

            public async Task<Series?> GetByIdAsync(Guid id)
            {
                return await context.Series.FindAsync(id);
            }

            public async Task<Series?> GetByIdWithDetailsAsync(Guid id)
            {
                return await context.Series
                   .Include(s => s.Episodes)
                   .FirstOrDefaultAsync(s => s.Id == id);
            }

            public async Task<List<Series>> GetPagedAsync(int page, int pageSize)
            {
                return await context.Series
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            public async Task<bool> UpdateAsync(Series obj)
            {
                context.Series.Update(obj);
                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
