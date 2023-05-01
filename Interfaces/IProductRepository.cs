namespace ClientesProdutos.Interfaces;

public interface IProductRepository
{
    Task<List<GetProductViewModel>> GetProducts();
    Task<GetProductViewModel> GetProduct(int id);
    Task AddProduct(AddProductViewModel newProduct);
    Task<GetProductViewModel> UpdateProduct(UpdateProductViewModel updatedProduct);
    Task<GetProductViewModel> RemoveProduct(int id);
}