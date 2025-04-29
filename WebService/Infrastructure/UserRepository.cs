using Microsoft.EntityFrameworkCore;
using WebService.Domain.Entities;
using WebService.Domain.Interface;
using WebService.Infrastructure.Data;

namespace WebService.Infrastructure
{
    namespace Repositories
    {
        public class UserRepository(AppDbContext context) : IUserRepository
        {
            public async Task<User> AddAsync(User obj)
            {
                await context.Users.AddAsync(obj);
                await context.SaveChangesAsync();
                return obj;
            }

            public async Task<bool> DeleteAsync(User obj)
            {
                context.Users.Remove(obj);
                return await context.SaveChangesAsync() > 0;
            }

            public async Task<User?> GetByIdAsync(Guid id)
            {
                return await context.Users.FindAsync(id);
            }

            public async Task<User?> GetByIdWithDetailsAsync(Guid id)
            {
                return await context.Users
                   .Include(u => u.Interactions)
                   .FirstOrDefaultAsync(u => u.Id == id);
            }

            public async Task<List<User>> GetPagedAsync(int page, int pageSize)
            {
                return await context.Users
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            public async Task<bool> UpdateAsync(User obj)
            {
                context.Users.Update(obj);
                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
