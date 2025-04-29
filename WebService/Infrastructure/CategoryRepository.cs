using Microsoft.EntityFrameworkCore;
using WebService.Domain.Entities;
using WebService.Domain.Interface;
using WebService.Infrastructure.Data;

namespace WebService.Infrastructure
{
    namespace Repositories
    {
        public class CategoryRepository(AppDbContext context) : ICategoryRepository
        {
            public async Task<Category> AddAsync(Category obj)
            {
                await context.Categories.AddAsync(obj);
                await context.SaveChangesAsync();
                return obj;
            }

            public async Task<bool> DeleteAsync(Category obj)
            {
                context.Categories.Remove(obj);
                return await context.SaveChangesAsync() > 0;
            }

            public async Task<Category?> GetByIdAsync(Guid id)
            {
                return await context.Categories.FindAsync(id);
            }

            public async Task<Category?> GetByIdWithDetailsAsync(Guid id)
            {
                return await context.Categories
                   .Include(c => c.SubCategories)
                   .Include(c => c.Parent)
                   .Include(c => c.Videos)
                   .FirstOrDefaultAsync(c => c.Id == id);
            }

            public async Task<List<Category>> GetPagedAsync(int page, int pageSize)
            {
                return await context.Categories
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            public async Task<List<Category>> ListAlByNamesync(string name)
            {
                return await context.Categories
                    .Where(c => EF.Functions.ILike(c.Name, $"%{name}%"))
                    .ToListAsync();
            }
            public async Task<bool> UpdateAsync(Category obj)
            {
                context.Categories.Update(obj);
                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
