namespace ClientesProdutos.ViewModels.Product;

public record GetProductViewModel
{
    [Key] public int Id { get; init; }
    public string Name { get; init; } = string.Empty!;
    public float Price { get; init; }
    public bool Active { get; init; }
}