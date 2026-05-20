namespace Application.DTOs;

public class CategoryUpdateDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid? ParentId { get; set; }
}