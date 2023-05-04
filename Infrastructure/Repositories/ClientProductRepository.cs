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

    // BUG
    /// <summary>
    ///     Linka tabelas de produtos
    ///     e tabela de clientes
    ///     com a tabela clients_products
    /// </summary>
    /// <returns>Pessoas e produtos comprados</returns>
    public async Task<List<GetClientProductViewModel>> GetClientProductsRelation()
    {
        var getClientProductsRelationQuery = @"SELECT cp.CLIENT_ID AS ClientId,
                                                      cp.PRODUCT_ID AS ProductId,
                                                      c.ID,
                                                      p.ID,
                                                      c.CLIENT_NAME AS ClientName, 
                                                      c.EMAIL AS Email,
                                                      p.PRODUCT_NAME AS ProductName,
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
                if (clientProduct.Clients == null) clientProduct.Clients = new List<Client>();

                if (clientProduct.Products == null) clientProduct.Products = new List<Product>();

                clientProduct.Clients.Add(client);
                clientProduct.Products.Add(product);

                return clientProduct;
            },
            splitOn: "Id");

        // Manual
        var result = clientsProducts.Select(x => new GetClientProductViewModel
        {
            ClientId = x.ClientId,
            ProductId = x.ProductId,
            Email = x.Clients.Select(x => x.Email).FirstOrDefault(),
            ClientName = x.Clients.Select(y => y.ClientName).FirstOrDefault(),
            ProductName = x.Products.Select(y => y.ProductName).FirstOrDefault(),
            Value = x.Products.Select(z => z.Value).FirstOrDefault()
        }).ToList();
        //

        // Using AutoMapper
        // var result = clientsProducts.Select(x => _mapper.Map<GetClientProductViewModel>(x)).ToList();

        _dbConnection.Close();

        return result;
    }
}