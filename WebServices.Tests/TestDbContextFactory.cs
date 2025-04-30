using Microsoft.EntityFrameworkCore;
using WebService.Infrastructure.Data;

namespace WebServices.Tests
{
    public class TestDbContextFactory
    {
        public static AppDbContext Create()
        {
            // Usa SQLite em memória para testes rápidos
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            var context = new AppDbContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated(); // Cria o schema

            return context;
        }

        public static void Destroy(AppDbContext context)
        {
            context.Database.CloseConnection();
            context.Dispose();
        }
    }
}
