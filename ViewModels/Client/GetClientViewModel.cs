namespace ClientesProdutos.ViewModels.Client;

public record GetClientViewModel
{
    [Key] public int Id { get; init; }
    public string ClientName { get; init; } = string.Empty!;

    public string LastName { get; init; } = string.Empty!;

    public string Email { get; init; } = string.Empty!;
    public bool Active { get; init; }
}