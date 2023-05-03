namespace ClientesProdutos.ViewModels.ClientProduct;

public record GetClientProductViewModel
{
    public int ClientId { get; init; }
    public int ProductId { get; init; }
    public string Name { get; init; } = string.Empty!;
    public string Email { get; init; } = string.Empty!;
    public float Value { get; init; }
}