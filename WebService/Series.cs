namespace WebService.Domain
{
    namespace Entities
    {
        public class Series : BaseEntity
        {
            public required string Title { get; set; }
            public string Description { get; set; } = string.Empty;
            public int TotalSeasons { get; set; }
            public int CurrentSeason { get; set; } = 1;
            public string? CoverImage { get; set; }

            public List<Video> Episodes { get; set; } = [];
        }
    }
}