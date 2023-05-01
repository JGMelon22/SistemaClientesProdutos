namespace ClientesProdutos.ViewModels.Product;

public record UpdateProductViewModel
{
    [Key] public int Id { get; init; }

    public string Name { get; init; } = string.Empty!;
    public float Value { get; init; }
    public bool Active { get; init; }
}