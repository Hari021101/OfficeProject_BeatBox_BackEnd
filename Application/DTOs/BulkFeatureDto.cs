namespace Application.DTOs;

public class BulkFeatureDto
{
    public IEnumerable<Guid> ProductIds { get; set; } = new List<Guid>();
    public bool IsFeatured { get; set; }
}
