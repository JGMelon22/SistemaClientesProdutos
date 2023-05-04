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
        var getClientProductsRelationQuery = @"SELECT cp.CLIENT_ID AS ClientId,
                                                      cp.PRODUCT_ID AS ProductId,
                                                      c.Id AS ClientId, 
                                                      c.Client_Name AS ClientName, 
                                                      c.Email, 
                                                      p.Id AS ProductId, 
                                                      p.Product_Name AS ProductName, 
                                                      p.Value
                                               FROM Clients_Products cp
                                               INNER JOIN Clients c 
                                                  ON cp.Client_Id = c.Id
                                               INNER JOIN Products p
                                                  ON cp.Product_Id = p.Id";

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