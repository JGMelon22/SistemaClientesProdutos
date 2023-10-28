namespace ClientesProdutos.ViewModels.Client;

public class AddClientViewModel
{
    public string ClientName { get; set; };

    [Column("LAST_NAME")] public string LastName { get; set; };

    public string Email { get; set; };
    public bool Active { get; set; }
}