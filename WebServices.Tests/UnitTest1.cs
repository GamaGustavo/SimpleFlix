using WebService.Domain.Entities;
using WebService.Infrastructure.Data;
using WebService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
namespace WebServices.Tests
{
    // VideoRepositoryTests.cs
    public class VideoRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly VideoRepository _repository;

        public VideoRepositoryTests()
        {
            _context = TestDbContextFactory.Create();
            _repository = new VideoRepository(_context);
        }

        [Fact]
        public async Task AddAsync_ShouldSaveVideoWithCategory()
        {
            // Arrange
            var category = new Category { Name = "Tutorials" };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            var video = new Video
            {
                Title = "Test Video",
                CategoryId = category.Id
            };

            // Act
            var savedVideo = await _repository.AddAsync(video);

            // Assert
            Assert.NotNull(savedVideo.Id);
            Assert.Equal(category.Id, savedVideo.CategoryId);

            // Verifica se a relação está correta
            var videoFromDb = await _context.Videos
                .Include(v => v.Category)
                .FirstOrDefaultAsync(v => v.Id == savedVideo.Id);

            Assert.NotNull(videoFromDb.Category);
            Assert.Equal("Tutorials", videoFromDb.Category.Name);
        }
        [Fact]
        public async Task UpdateAsync_ShouldChangeVideoTitle()
        {
            // Arrangeet
            var video = new Video { Title = "Old Title" };
            await _repository.AddAsync(video);

            // Act
            video.Title = "New Title";
            await _repository.UpdateAsync(video);

            // Assert
            var updatedVideo = await _repository.GetByIdAsync(video.Id);
            Assert.Equal("New Title", updatedVideo.Title);
        }
        [Fact]
        public async Task DeleteAsync_ShouldRemoveVideo()
        {
            // Arrange
            var video = new Video { Title = "To Delete" };
            await _repository.AddAsync(video);

            // Act
            await _repository.DeleteAsync(video);

            // Assert
            var deletedVideo = await _repository.GetByIdAsync(video.Id);
            Assert.Null(deletedVideo);
        }
        [Fact]
        public async Task AddVideoWithGenres_ShouldSaveCorrectly()
        {
            // Arrange
            var actionGenre = new Genre { Name = "Ação" };
            var documentaryGenre = new Genre { Name = "Documentário" };

            var video = new Video
            {
                Title = "Video com Gêneros",
                Genres = new List<VideoGenre>
            {
                new VideoGenre { Genre = actionGenre },
                new VideoGenre { Genre = documentaryGenre }
            }
            };

            // Act
            _context.Videos.Add(video);
            await _context.SaveChangesAsync();

            // Assert
            var savedVideo = await _context.Videos
                .Include(v => v.Genres)
                    .ThenInclude(vg => vg.Genre)
                .FirstOrDefaultAsync(v => v.Id == video.Id);

            Assert.NotNull(savedVideo);
            Assert.Equal(2, savedVideo.Genres.Count);
            Assert.Contains(savedVideo.Genres, g => g.Genre.Name == "Ação");
            Assert.Contains(savedVideo.Genres, g => g.Genre.Name == "Documentário");
        }


        public void Dispose() => TestDbContextFactory.Destroy(_context);
    }
}