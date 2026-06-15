using System.Collections.Generic;

namespace Application.DTOs;

public class BulkOrderStatusUpdateDto
{
    public List<int> OrderIds { get; set; } = new List<int>();
    public string Status { get; set; } = string.Empty;
}
