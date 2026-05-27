namespace Domain.Entities;

public class ProductFaq
{
    public int Id { get; set; }

    public Guid ProductId { get; set; }

    public string Question { get; set; }

    public string Answer { get; set; }

    public Product Product { get; set; }
}