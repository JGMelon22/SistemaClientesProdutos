namespace ClientesProdutos.ViewModels.Product;

public record AddProductViewModel
{
    public required string ProductName { get; init; }
    public float Value { get; init; }
    public bool Active { get; init; }
}