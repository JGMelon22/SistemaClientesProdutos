namespace ClientesProdutos.ViewModels.ClientProduct;

public record GetClientProductViewModel
{
    public int ClientId { get; init; }
    public int ProductId { get; init; }
    public required string ClientName { get; init; }
    public required string Email { get; init; }
    public required string ProductName { get; set; }
    public float Value { get; init; }
}