using System;

namespace Application.DTOs;

public class StockNotificationRequestDto
{
    public Guid ProductId { get; set; }
    public Guid VariantId { get; set; }
}
