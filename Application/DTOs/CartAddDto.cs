namespace Application.DTOs;

public class CartAddDto
{
    public Guid ProductId { get; set; }
    public Guid VariantId { get; set; }

    public int Quantity { get; set; }

    public bool IsPersonalised { get; set; }
    public string? EngravingName { get; set; }
    public string? EngravingDate { get; set; }
    public string? EngravingMessage { get; set; }

}