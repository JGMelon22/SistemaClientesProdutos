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

    public override Task<List<GetProductViewModel>> SortModel(string sortOrder)
    {
        throw new NotImplementedException();
    }
}