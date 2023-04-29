namespace ClientesProdutos.Domain.Interfaces;

public interface IClientRepository
{
    Task<List<GetClientViewModel>> GetClients();
    Task<GetClientViewModel> GetClient(int id);
    Task<GetClientViewModel> UpdateClient(UpdateClientViewModel updatedClient);
    Task<GetClientViewModel> RemoveClient(int id);
}