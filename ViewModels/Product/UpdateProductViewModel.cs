namespace ClientesProdutos.ViewModels.Product;

public record UpdateProductViewModel
{
    [Key] public int Id { get; init; }

    public required string ProductName { get; init; }
    public float Value { get; init; }
    public bool Active { get; init; }
}