namespace ClientesProdutos.Services;

public class SortingClientService : SortingService<GetClientViewModel>
{
    private readonly IDbConnection _dbConnection;
    private readonly IMapper _mapper;

    public SortingClientService(IDbConnection dbConnection, IMapper mapper)
    {
        _dbConnection = dbConnection;
        _mapper = mapper;
    }

    public override async Task<List<GetClientViewModel>> SortModel(string sortOrder)
    {
        var getClientsOrderByAscQuery = @"SELECT ID,
                                                 CLIENT_NAME AS ClientName,
                                                 LAST_NAME AS LastName,
                                                 EMAIL,
                                                 ACTIVE
                                          FROM CLIENTS";

        _dbConnection.Open();

        var result = await _dbConnection.QueryAsync<Client>(getClientsOrderByAscQuery);
        var mappedResult = result.Select(x => _mapper.Map<GetClientViewModel>(x)).ToList().OrderBy(x => x.ClientName);

        switch (sortOrder)
        {
            case "name_desc":
                mappedResult = mappedResult.OrderByDescending(x => x.ClientName);
                break;

            default:
                mappedResult = mappedResult.OrderBy(x => x.ClientName);
                break;
        }

        _dbConnection.Close();

        return mappedResult.ToList();
    }
}