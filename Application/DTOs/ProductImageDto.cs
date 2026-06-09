namespace Application.DTOs;

public class ProductImageDto
{
    public string ImageUrl { get; set; }

    public bool IsPrimary { get; set; }

    public string ColorName { get; set; } = string.Empty;

    public string ColorCode { get; set; } = string.Empty;
}