using ClientesProdutos.Domain.Entities;
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
                                       NAME, 
                                       LAST_NAME,
                                       EMAIL,
                                       ACTIVE 
                                FROM CLIENTS;";

        _dbConnection.Open();

        var result = await _dbConnection.QueryAsync<Client>(getClientsQuery);
        var mappedResult = result.Select(x => _mapper.Map<GetClientViewModel>(x)).ToList();

        _dbConnection.Close();

        return mappedResult;
    }

    public async Task<GetClientViewModel> GetClient(int id)
    {
        var getClientByIdQuery = @"SELECT ID, 
                                       NAME, 
                                       LAST_NAME,
                                       EMAIL,
                                       ACTIVE 
                                FROM CLIENTS
                                WHERE ID = @Id;";
        _dbConnection.Open();

        var result = await _dbConnection.QueryFirstOrDefaultAsync<Client>(getClientByIdQuery, new { Id = id });
        var mappedResult = _mapper.Map<GetClientViewModel>(result);

        _dbConnection.Close();

        return mappedResult;
    }

    public async Task AddClient(AddClientViewModel newClient)
    {
        var addClientQuery = @"INSERT INTO CLIENTS(NAME, LAST_NAME, EMAIL, ACTIVE)
                               VALUES(@Name, @LastName, @Email, @Active);";

        var client = _mapper.Map<Client>(newClient);

        _dbConnection.Open();

        await _dbConnection.ExecuteAsync(addClientQuery, client);

        _dbConnection.Close();
    }

    public async Task<GetClientViewModel> UpdateClient(UpdateClientViewModel updatedClient)
    {
        var findClient = @"SELECT ID, 
                                       NAME, 
                                       LAST_NAME,
                                       EMAIL,
                                       ACTIVE 
                                FROM CLIENTS
                                WHERE ID = @Id;";

        var updateClienteQuery = @"UPDATE CLIENTS
                                   SET NAME=@Name, 
                                       LAST_NAME=@LastName, 
                                       EMAIL=@Email, 
                                       ACTIVE=@Active
                                   WHERE ID=@Id;";
        _dbConnection.Open();

        var client =
            await _dbConnection.QueryFirstOrDefaultAsync<Client>(findClient, new { updatedClient.Id });

        if (client == null)
        {
            _dbConnection.Close();
            return null;
        }

        _mapper.Map(updatedClient, client);

        await _dbConnection.ExecuteAsync(updateClienteQuery, client);

        var mappedClient = _mapper.Map<GetClientViewModel>(client);

        _dbConnection.Close();

        return mappedClient;
    }

    public async Task<GetClientViewModel> RemoveClient(int id)
    {
        var findClient = @"SELECT ID, 
                                  NAME, 
                                  LAST_NAME,
                                  EMAIL,
                                  ACTIVE 
                           FROM CLIENTS
                           WHERE ID = @Id;";

        var removeClientQuery = @"DELETE 
                                  FROM CLIENT
                                  WHERE ID = @Id;";

        _dbConnection.Open();

        var client = await _dbConnection.QueryFirstOrDefaultAsync<Client>(findClient, new { Id = id });

        if (client == null)
        {
            _dbConnection.Close();
            return null;
        }

        await _dbConnection.ExecuteAsync(removeClientQuery, id);

        var mappedClient = _mapper.Map<GetClientViewModel>(client);

        _dbConnection.Close();

        return mappedClient;
    }
}