using WebService.Domain.Enuns;

namespace WebService.Domain
{
    namespace Entities
    {
        public class User : BaseEntity
        {
            public required string Email { get; set; }
            public string? Username { get; set; }
            public byte[] PasswordHash { get; set; } = [];
            public byte[] PasswordSalt { get; set; } = [];
            public string? RefreshToken { get; set; }
            public DateTime? RefreshTokenExpiry { get; set; }

            public List<VideoInteraction> Interactions { get; set; } = new();
        }

        public class VideoInteraction : BaseEntity
        {
            public Guid UserId { get; set; }
            public User? User { get; set; }

            public Guid VideoId { get; set; }
            public Video? Video { get; set; }

            public InteractionType Type { get; set; }
            public int? ProgressSeconds { get; set; } // Progresso de assistência
        }
    }

    namespace Enuns
    {
        public enum InteractionType
        {
            View,
            Like,
            WatchLater
        }
    }
}
