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

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var client = await _repository.GetClient(id);
        return client != null
            ? await Task.Run(() => View(client))
            : NotFound();
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var clientToRemove = await _repository.RemoveClient(id);
        return clientToRemove != null
            ? await Task.Run(() => RedirectToAction(nameof(Index)))
            : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return await Task.Run(() => View());
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddClientViewModel newClient)
    {
        if (!ModelState.IsValid)
            return View(nameof(Create));

        await _repository.AddClient(newClient);

        return await Task.Run(() => RedirectToAction(nameof(Index)));
    }

    // TODO -
    /* AddClient
     * UpdateClientValidator 
     */
}