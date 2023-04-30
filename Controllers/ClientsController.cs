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

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        if (id == null)
            return NotFound();

        var client = await _repository.GetClient(id);
        return client != null
            ? await Task.Run(() => View(client))
            : NotFound();
    }

    // TODO -
    /* Get Client
     * AddClient
     * UpdateClientValidator
     * RemoveClient 
     */
}