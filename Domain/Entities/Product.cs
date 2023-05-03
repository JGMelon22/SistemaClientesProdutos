namespace ClientesProdutos.Domain.Entities;

public class Product : BaseModel
{
    public string Name { get; set; } = string.Empty!;
    public float Value { get; set; }
    public bool Active { get; set; }
    [ForeignKey(nameof(IdProduct))] public int IdProduct { get; set; }
    public ClientProduct? ClientProduct { get; set; }
}