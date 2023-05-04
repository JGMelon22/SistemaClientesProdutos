namespace ClientesProdutos.ViewModels.Client;

public class AddClientViewModel
{
    public string ClientName { get; set; } = string.Empty!;

    [Column("LAST_NAME")] public string LastName { get; set; } = string.Empty!;

    public string Email { get; set; } = string.Empty!;
    public bool Active { get; set; }
}