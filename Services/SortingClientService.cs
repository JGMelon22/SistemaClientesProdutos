using ClientesProdutos.Interfaces;

namespace ClientesProdutos.Services;

public class SortingClientService : ISortingClientService
{
    private readonly IDbConnection _dbConnection;
    private readonly IMapper _mapper;

    public SortingClientService(IDbConnection dbConnection, IMapper mapper)
    {
        _dbConnection = dbConnection;
        _mapper = mapper;
    }

    public async Task<List<GetClientViewModel>> SortClient(string sortOrder)
    {
        var getClientsOrderByAscQuery = @"SELECT ID,
                                                 NAME,
                                                 LAST_NAME AS LastName,
                                                 EMAIL,
                                                 ACTIVE
                                          FROM CLIENTS";

        _dbConnection.Open();

        var result = await _dbConnection.QueryAsync<Client>(getClientsOrderByAscQuery);
        var mappedResult = result.Select(x => _mapper.Map<GetClientViewModel>(x)).ToList().OrderBy(x => x.Name);

        switch (sortOrder)
        {
            case "name_desc":
                mappedResult = mappedResult.OrderByDescending(x => x.Name);
                break;

            default:
                mappedResult = mappedResult.OrderBy(x => x.Name);
                break;
        }

        _dbConnection.Close();

        return mappedResult.ToList();
    }
}