namespace ClientesProdutos.Interfaces;

public interface IClientRepository
{
    Task<List<GetClientViewModel>> GetClients();
    Task<GetClientViewModel> GetClient(int id);
    Task AddClient(AddClientViewModel newClient);
    Task<GetClientViewModel> UpdateClient(UpdateClientViewModel updatedClient);
    Task<GetClientViewModel> RemoveClient(int id);
}