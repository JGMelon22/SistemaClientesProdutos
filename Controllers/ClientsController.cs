using ClientesProdutos.Domain.Entities;
using ClientesProdutos.Infrastructure.Repositories;

namespace ClientesProdutos.Controllers;

public class ClientsController : Controller
{
    private readonly ClientRepository _repository;

    public ClientsController(ClientRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetClients()
    {
        var clients = await _repository.GetClients();
        return clients != null
            ? Ok(clients)
            : NoContent();
        
    // TODO -
    /* Get Client
     * AddClient
     * UpdateClient
     * RemoveClient 
     */
    }
}