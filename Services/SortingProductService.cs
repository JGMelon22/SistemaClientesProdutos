namespace ClientesProdutos.Services;

public class SortingProductService : SortingService<GetProductViewModel>
{
    private readonly IDbConnection _dbConnection;
    private readonly IMapper _mapper;

    public SortingProductService(IDbConnection dbConnection, IMapper mapper)
    {
        _dbConnection = dbConnection;
        _mapper = mapper;
    }

    public override async Task<List<GetProductViewModel>> SortModel(string sortOrder)
    {
        var getProductsQuery = @"SELECT ID,
                                        PRODUCT_NAME AS ProductName,
                                        VALUE,
                                        ACTIVE
                                 FROM PRODUCTS";

        _dbConnection.Open();

        var result = await _dbConnection.QueryAsync<Product>(getProductsQuery);
        var mappedResult = result.Select(x => _mapper.Map<GetProductViewModel>(x)).ToList().OrderBy(x => x.ProductName);

        switch (sortOrder)
        {
            case "name_desc":
                mappedResult = mappedResult.OrderByDescending(x => x.ProductName);
                break;

            default:
                mappedResult = mappedResult.OrderBy(x => x.ProductName);
                break;
        }

        _dbConnection.Close();

        return mappedResult.ToList();
    }
}