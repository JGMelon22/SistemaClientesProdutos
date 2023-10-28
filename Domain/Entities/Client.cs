namespace ClientesProdutos.Domain.Entities;

public class Client
{
    [Key] public int Id { get; set; }

    public string ClientName { get; set; } = string.Empty!;
    public string LastName { get; set; } = string.Empty!;
    public string Email { get; set; } = string.Empty!;
    public bool Active { get; set; }

    [ForeignKey("ClientId")] public int ClientId { get; set; }

    public ClientProduct? ClientProduct { get; set; }
}