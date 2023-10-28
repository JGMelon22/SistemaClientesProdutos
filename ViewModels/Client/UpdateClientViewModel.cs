namespace ClientesProdutos.ViewModels.Client;

public class UpdateClientViewModel
{
    [Key] public int Id { get; set; }
    public string ClientName { get; set; };
    public string LastName { get; set; };
    public string Email { get; set; };
    public bool Active { get; set; }
}