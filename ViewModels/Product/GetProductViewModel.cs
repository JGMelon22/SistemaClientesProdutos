namespace ClientesProdutos.ViewModels.Product;

public record GetProductViewModel
{
    [Key] public int Id { get; init; }
    public string ProductName { get; init; } = string.Empty!;
    public float Value { get; init; }
    public bool Active { get; init; }
}