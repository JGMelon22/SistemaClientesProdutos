namespace ClientesProdutos.Domain.Entities;

public class Client : BaseModel
{
    public string Name { get; set; } = string.Empty!;
    public string LastName { get; set; } = string.Empty!;
    public string Email { get; set; } = string.Empty!;
    public bool Active { get; set; }
}