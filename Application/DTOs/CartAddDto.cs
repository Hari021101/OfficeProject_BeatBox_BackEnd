namespace Application.DTOs;

public class CartAddDto
{
    public Guid ProductId { get; set; }
    public Guid VariantId { get; set; }

    public int Quantity { get; set; }


}