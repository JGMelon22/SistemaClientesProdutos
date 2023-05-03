namespace ClientesProdutos.ViewModels.ClientProduct;

public record GetClientProductViewModel
{
    public string Name { get; set; } = string.Empty!;
    public string LastName { get; set; } = string.Empty!;
    public string Email { get; set; } = string.Empty!;
    public bool Active { get; set; }
    public float Value { get; set; }
    public int IdClient { get; init; }
    public int IdProduct { get; init; }
}