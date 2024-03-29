using ClientesProdutos.Interfaces;

namespace ClientesProdutos.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly IMapper _mapper;

    public ProductRepository(IDbConnection dbConnection, IMapper mapper)
    {
        _dbConnection = dbConnection;
        _mapper = mapper;
    }

    public async Task<List<GetProductViewModel>> GetProducts()
    {
        var getProductsQuery = @"SELECT ID,
                                        PRODUCT_NAME AS ProductName,
                                        VALUE,
                                        ACTIVE
                                 FROM PRODUCTS";

        _dbConnection.Open();

        var result = await _dbConnection.QueryAsync<Product>(getProductsQuery);
        var mappedResult = result.Select(x => _mapper.Map<GetProductViewModel>(x)).ToList();

        _dbConnection.Close();

        return mappedResult;
    }

    public async Task<GetProductViewModel> GetProduct(int id)
    {
        var getProductByIdQuery = @"SELECT ID,
                                        PRODUCT_NAME ProductName,
                                        VALUE,
                                        ACTIVE
                                 FROM PRODUCTS
                                 WHERE ID = :Id";

        var result = await _dbConnection.QueryFirstOrDefaultAsync<Product>(getProductByIdQuery, new { Id = id });
        var mappedResult = _mapper.Map<GetProductViewModel>(result);

        _dbConnection.Close();

        return mappedResult;
    }

    public async Task AddProduct(AddProductViewModel newProduct)
    {
        var addProductQuery = @"INSERT INTO PRODUCTS(PRODUCT_NAME, VALUE, ACTIVE)
                                VALUES(:ProductName, :Value, :Active)";

        var product = _mapper.Map<Product>(newProduct);

        // Cast to Oracle
        var activeValue = product.Active ? 1 : 0;

        _dbConnection.Open();

        await _dbConnection.ExecuteAsync(addProductQuery,
            new { product.ProductName, product.Value, Active = activeValue });

        _dbConnection.Close();
    }

    public async Task<GetProductViewModel> UpdateProduct(UpdateProductViewModel updatedProduct)
    {
        var findProductQuery = @"SELECT ID,
                                   PRODUCT_NAME AS ProductName,
                                   VALUE,
                                   ACTIVE
                            FROM PRODUCTS
                            WHERE ID = :Id";

        var updateProductQuery = @"UPDATE PRODUCTS
                                   SET PRODUCT_NAME = :ProductName, 
                                   VALUE = :Value,
                                   ACTIVE = :Active
                                   WHERE ID = :Id";

        _dbConnection.Open();

        var product =
            await _dbConnection.QueryFirstOrDefaultAsync<Product>(findProductQuery, new { updatedProduct.Id });

        if (product == null)
        {
            _dbConnection.Close();
            return null;
        }

        // Cast to Oracle
        var activeValue = updatedProduct.Active ? 1 : 0;

        _mapper.Map(updatedProduct, product);

        // Atenção! Usamos updatedProduct.Id para indicar ao Dapper que nossa chave no update é o Id
        await _dbConnection.ExecuteAsync(updateProductQuery,
            new { product.ProductName, product.Value, Active = activeValue, updatedProduct.Id });

        var mappedProduct = _mapper.Map<GetProductViewModel>(product);

        _dbConnection.Close();

        return mappedProduct;
    }

    public async Task RemoveProduct(int id)
    {
        var removeProductQuery = @"DELETE 
                                   FROM PRODUCTS
                                   WHERE ID = :Id";

        _dbConnection.Open();

        await _dbConnection.ExecuteAsync(removeProductQuery, new { Id = id });

        _dbConnection.Close();
    }
}