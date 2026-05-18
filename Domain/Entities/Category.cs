namespace Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid? ParentId { get; set; }

        public Category? ParentCategory { get; set; }

        public ICollection<Category>? SubCategories { get; set; }
    }
}
