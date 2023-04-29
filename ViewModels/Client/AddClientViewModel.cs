namespace ClientesProdutos.ViewModels.Client;

public class AddClientViewModel
{
    public string Name { get; set; } = string.Empty!;
    public string LastName { get; set; } = string.Empty!;
    public string Email { get; set; } = string.Empty!;
    public bool Active { get; set; }
}