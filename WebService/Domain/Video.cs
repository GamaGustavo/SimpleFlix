using WebService.Domain.Enuns;

namespace WebService.Domain
{
    namespace Entities
    {
        public class Video : BaseEntity
        {
            public required string Title { get; set; }
            public string Description { get; set; } = string.Empty;

            // Tipo de mídia (Filme, Episódio de Série, Documentário, etc.)
            public MediaType MediaType { get; set; }

            // Controle de arquivo vs link
            public bool IsFile { get; set; }
            public string? FilePath { get; set; } // Caminho no bucket (ex: "videos/{id}.mp4")
            public string? ExternalUrl { get; set; } // URL externa
            public string? ThumbnailPath { get; set; } // Caminho da thumbnail

            // Metadados
            public TimeSpan Duration { get; set; }
            public int? ReleaseYear { get; set; }
            public string? Director { get; set; }
            public VideoStatus Status { get; set; } = VideoStatus.Processing;

            // Relacionamentos
            public Guid? CategoryId { get; set; }
            public Category? Category { get; set; }

            public Guid? SeriesId { get; set; } // Para episódios de séries
            public Series? Series { get; set; }

            public List<VideoGenre> Genres { get; set; } = [];

            public int? EpisodeNumber { get; set; } // Número do episódio na temporada
            public int? SeasonNumber { get; set; } // Número da temporada
        }
    }

    namespace Enuns
    {
        public enum MediaType
        {
            Movie,
            SeriesEpisode,
            Documentary,
            Animation,
            ShortFilm
        }


        public enum VideoStatus
        {
            Processing,
            Available,
            Blocked
        }
    }
}