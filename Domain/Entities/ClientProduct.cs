namespace ClientesProdutos.Domain.Entities;

public class ClientProduct
{
    public int ClientId { get; set; }
    public int ProductId { get; set; }
    public List<Product>? Products { get; set; }
    public List<Client>? Clients { get; set; }
}