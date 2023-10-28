// Backup

using ClientesProdutos.ViewModels.ClientProduct;

namespace ClientesProdutos.Infrastructure.Repositories;

public class ClientProductRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly IMapper _mapper;

    public ClientProductRepository(IDbConnection dbConnection, IMapper mapper)
    {
        _dbConnection = dbConnection;
        _mapper = mapper;
    }

    public async Task<List<GetClientProductViewModel>> GetClientProductsRelation()
    {
        var getClientProductsRelationQuery = @"SELECT DISTINCT (p.Product_Name) AS ProductName,
                                                                p.Id AS ProductId,
                                                                p.Value,
                                                                c.Client_Name AS ClientName, 
                                                                c.Id AS ClientId, 
                                                                c.Email,
                                                                cp.CLIENT_ID AS ClientId,
                                                                cp.PRODUCT_ID AS ProductId
                                               FROM PRODUCTS p
                                               INNER JOIN CLIENTS_PRODUCTS cp
                                               	ON p.ID = cp.PRODUCT_ID 
                                               INNER JOIN CLIENTS c 
                                               	ON c.ID = cp.CLIENT_ID;";

        _dbConnection.Open();

        var clientsProducts = await _dbConnection.QueryAsync<ClientProduct, Client, Product, ClientProduct>(
            getClientProductsRelationQuery,
            (clientProduct, client, product) =>
            {
                if (clientProduct.Clients == null) clientProduct.Clients = new List<Client>();

                if (clientProduct.Products == null) clientProduct.Products = new List<Product>();

                clientProduct.Clients.Add(client);
                clientProduct.Products.Add(product);

                return clientProduct;
            },
            splitOn: "Id, ClientId, ProductId");

        // Using AutoMapper
        var result = clientsProducts.Select(x => _mapper.Map<GetClientProductViewModel>(x)).ToList();

        _dbConnection.Close();

        return result;
    }
}