namespace ClientesProdutos.Domain.Entities;

public class Product
{
    [Key] public int Id { get; set; }

    public string ProductName { get; set; } = string.Empty!;
    public float Value { get; set; }
    public bool Active { get; set; }

    [ForeignKey("ProductId")] public int ProductId { get; set; }

    public ClientProduct? ClientProduct { get; set; }
}