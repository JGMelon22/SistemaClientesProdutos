namespace ClientesProdutos.Domain.Entities;

public class Product : BaseModel
{
    public string Name { get; set; } = string.Empty!;
    public float Price { get; set; }
    public bool Active { get; set; }
}