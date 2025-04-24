using Microsoft.EntityFrameworkCore;
using WebService.Domain.Entities;
using WebService.Domain.Enuns;

namespace WebService.Infrastructure
{
    namespace Data
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            public AppDbContext() { }

            public DbSet<Video> Videos => Set<Video>();
            public DbSet<Category> Categories => Set<Category>();
            public DbSet<Series> Series => Set<Series>();
            public DbSet<Genre> Genres => Set<Genre>();
            public DbSet<VideoGenre> VideoGenres => Set<VideoGenre>();
            public DbSet<User> Users => Set<User>();
            public DbSet<VideoInteraction> VideoInteractions => Set<VideoInteraction>();

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Configurações de relacionamento
                modelBuilder.Entity<Video>()
                    .HasOne(v => v.Category)
                    .WithMany(c => c.Videos)
                    .HasForeignKey(v => v.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<VideoGenre>()
                    .HasKey(vg => new { vg.VideoId, vg.GenreId });

                // Índices para otimização
                modelBuilder.Entity<Video>()
                    .HasIndex(v => v.CreatedAt)
                    .IsDescending();

                modelBuilder.Entity<Video>()
                    .HasIndex(v => v.Title)
                    .HasMethod("GIN")
                    .IsTsVectorExpressionIndex("portuguese");

                // Configuração do PostgreSQL para enums
                modelBuilder.HasPostgresEnum<MediaType>();
                modelBuilder.HasPostgresEnum<VideoStatus>();
                modelBuilder.HasPostgresEnum<InteractionType>();
            }
        }
    }
}
