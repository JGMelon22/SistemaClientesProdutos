using ClientesProdutos.Interfaces;

namespace ClientesProdutos.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly IMapper _mapper;

    public ClientRepository(IDbConnection dbConnection, IMapper mapper)
    {
        _dbConnection = dbConnection;
        _mapper = mapper;
    }

    public async Task<List<GetClientViewModel>> GetClients()
    {
        var getClientsQuery = @"SELECT ID, 
                                       CLIENT_NAME AS ClientName, 
                                       LAST_NAME AS LastName,
                                       EMAIL,
                                       ACTIVE 
                                FROM CLIENTS";

        _dbConnection.Open();

        var result = await _dbConnection.QueryAsync<Client>(getClientsQuery);
        var mappedResult = result.Select(x => _mapper.Map<GetClientViewModel>(x)).ToList();

        _dbConnection.Close();

        return mappedResult;
    }

    public async Task<GetClientViewModel> GetClient(int id)
    {
        var getClientByIdQuery = @"SELECT ID, 
                                          CLIENT_NAME AS ClientName,
                                          LAST_NAME AS LastName,
                                          EMAIL,
                                          ACTIVE 
                                   FROM CLIENTS
                                   WHERE ID = :Id";
        _dbConnection.Open();

        var result = await _dbConnection.QueryFirstOrDefaultAsync<Client>(getClientByIdQuery, new { Id = id });
        var mappedResult = _mapper.Map<GetClientViewModel>(result);

        _dbConnection.Close();

        return mappedResult;
    }

    public async Task AddClient(AddClientViewModel newClient)
    {
        var addClientQuery = @"INSERT INTO CLIENTS(CLIENT_NAME, LAST_NAME, EMAIL, ACTIVE)
                               VALUES(:ClientName, :LastName, :Email, :Active)";

        var client = _mapper.Map<Client>(newClient);

        // Cast to Oracle
        var activeValue = client.Active ? 1 : 0;

        _dbConnection.Open();

        await _dbConnection.ExecuteAsync(addClientQuery,
            new { client.ClientName, client.LastName, client.Email, Active = activeValue });

        _dbConnection.Close();
    }

    public async Task<GetClientViewModel> UpdateClient(UpdateClientViewModel updatedClient)
    {
        var findClientQuery = @"SELECT ID, 
                                  CLIENT_NAME AS ClientName, 
                                  LAST_NAME AS LastName,
                                  EMAIL,
                                  ACTIVE 
                                FROM CLIENTS
                                WHERE ID = :Id";

        var updateClienteQuery = @"UPDATE CLIENTS
                                   SET CLIENT_NAME = :ClientName, 
                                       LAST_NAME =:LastName, 
                                       EMAIL=:Email, 
                                       ACTIVE=:Active
                                   WHERE ID=:Id";
        _dbConnection.Open();

        var client =
            await _dbConnection.QueryFirstOrDefaultAsync<Client>(findClientQuery, new { updatedClient.Id });

        if (client == null)
        {
            _dbConnection.Close();
            return null;
        }

        // Cast to Oracle
        var activeValue = updatedClient.Active ? 1 : 0;

        _mapper.Map(updatedClient, client);

        await _dbConnection.ExecuteAsync(updateClienteQuery,
            new { client.ClientName, client.LastName, client.Email, Active = activeValue, updatedClient.Id });

        var mappedClient = _mapper.Map<GetClientViewModel>(client);

        _dbConnection.Close();

        return mappedClient;
    }

    public async Task RemoveClient(int id)
    {
        var removeClientQuery = @"DELETE 
                                  FROM CLIENTS
                                  WHERE ID = :Id";

        _dbConnection.Open();

        await _dbConnection.ExecuteAsync(removeClientQuery, new { Id = id });

        _dbConnection.Close();
    }
}