using ClientesProdutos.Interfaces;

namespace ClientesProdutos.Controllers;

public class ClientsController : Controller
{
    private readonly IClientRepository _repository;

    public ClientsController(IClientRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var clients = await _repository.GetClients();
        return clients != null
            ? await Task.Run(() => View(clients))
            : NoContent();
    }

    // TODO -
    /* Get Client
     * AddClient
     * UpdateClientValidator
     * RemoveClient 
     */
}