namespace WebService.Domain
{
    namespace Entities
    {
        public class Category : BaseEntity
        {
            public required string Name { get; set; }
            public string? Icon { get; set; } // URL para ícone

            // Hierarquia de categorias
            public Guid? ParentId { get; set; }
            public Category? Parent { get; set; }
            public List<Category> SubCategories { get; set; } = [];

            public List<Video> Videos { get; set; } = [];
        }
    }
}