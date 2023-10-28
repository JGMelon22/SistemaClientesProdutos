namespace ClientesProdutos.ViewModels.Client;

public record AddClientViewModel
{
    public required string ClientName { get; init; }

    [Column("LAST_NAME")] public required string LastName { get; init; }

    public required string Email { get; init; }
    public bool Active { get; init; }
}