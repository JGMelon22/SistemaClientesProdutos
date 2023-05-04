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
                                                      c.ID,
                                                      p.ID,
                                                      c.NAME,
                                                      c.EMAIL,
                                                      p.NAME,
                                                      p.VALUE
                                                  FROM CLIENTS_PRODUCTS cp 
                                                  INNER JOIN CLIENTS c 
                                                  	ON cp.CLIENT_ID  = c.ID 
                                                  INNER JOIN PRODUCTS p 
                                                  	ON cp.PRODUCT_ID = p.ID";

        _dbConnection.Open();

        var clientsProducts = await _dbConnection.QueryAsync<ClientProduct, Client, Product, ClientProduct>(
            getClientProductsRelationQuery,
            (clientProduct, client, product) =>
            {
                if (clientProduct.Clients == null)
                    clientProduct.Clients = new List<Client>();

                if (clientProduct.Products == null)
                    clientProduct.Products = new List<Product>();

                clientProduct.Clients.Add(client);
                clientProduct.Products.Add(product);

                return clientProduct;
            },
            splitOn: "Id, Name");

        // Using AutoMapper
        var result = clientsProducts.Select(x => _mapper.Map<GetClientProductViewModel>(x)).ToList();

        _dbConnection.Close();

        return result;
    }
}