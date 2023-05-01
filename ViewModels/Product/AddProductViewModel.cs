namespace ClientesProdutos.ViewModels.Product;

public record AddProductViewModel
{
    public string Name { get; init; } = string.Empty!;
    public float Price { get; init; }
    public bool Active { get; init; }
}