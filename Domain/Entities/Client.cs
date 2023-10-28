namespace ClientesProdutos.Domain.Entities;

public class Client
{
    [Key] public int Id { get; set; }

    public required string ClientName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public bool Active { get; set; }

    [ForeignKey("ClientId")] public int ClientId { get; set; }

    public ClientProduct? ClientProduct { get; set; }
}