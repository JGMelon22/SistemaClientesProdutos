namespace ClientesProdutos.ViewModels.Client;

public record UpdateClientViewModel
{
    [Key] public int Id { get; init; }
    public required string ClientName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public bool Active { get; init; }
}