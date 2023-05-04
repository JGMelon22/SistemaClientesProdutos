namespace ClientesProdutos.ViewModels.Product;

public record AddProductViewModel
{
    public string ProductName { get; init; } = string.Empty!;
    public float Value { get; init; }
    public bool Active { get; init; }
}