namespace WebService.Domain
{
    namespace Entities
    {
        public class Genre : BaseEntity
        {
            public required string Name { get; set; }
            public List<VideoGenre> Videos { get; set; } = new();
        }

        // Entidade de junção para relação N-N
        public class VideoGenre
        {
            public Guid VideoId { get; set; }
            public Video? Video { get; set; }

            public Guid GenreId { get; set; }
            public Genre? Genre { get; set; }
        }
    }
}

