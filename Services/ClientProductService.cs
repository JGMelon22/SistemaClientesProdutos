using ClientesProdutos.ViewModels.ClientProduct;

namespace ClientesProdutos.Services;

public class ClientProductService : SortingService<GetClientProductViewModel>
{
    private readonly IDbConnection _dbcConnection;
    private readonly IMapper _mapper;

    public ClientProductService(IDbConnection dbcConnection, IMapper mapper)
    {
        _dbcConnection = dbcConnection;
        _mapper = mapper;
    }

    public override async Task<List<GetClientProductViewModel>> SortModel(string sortOrder)
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

        _dbcConnection.Open();

        var clientsProducts = await _dbcConnection.QueryAsync<ClientProduct, Client, Product, ClientProduct>(
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

        var mappedResult = clientsProducts.Select(x => _mapper.Map<GetClientProductViewModel>(x)).ToList()
            .OrderBy(x => x.ClientName);

        // Orderna baseado no conteudo da ViewBag
        switch (sortOrder)
        {
            case "name_desc":
                mappedResult = mappedResult.OrderByDescending(x => x.ClientName);
                break;

            default:
                mappedResult = mappedResult.OrderBy(x => x.ClientName);
                break;
        }

        _dbcConnection.Close();

        return mappedResult.ToList();
    }
}