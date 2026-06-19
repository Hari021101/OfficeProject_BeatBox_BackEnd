namespace Application.DTOs;

public class CategoryResponseDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid? ParentId { get; set; }

    public string? ImageUrl { get; set; }

    public int ProductCount { get; set; }
}