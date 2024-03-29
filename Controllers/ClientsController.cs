using ClientesProdutos.Interfaces;
using ClientesProdutos.Services;

namespace ClientesProdutos.Controllers;

public class ClientsController : Controller
{
    private readonly IClientRepository _repository;
    private readonly SortingService<GetClientViewModel> _sortingClient;

    public ClientsController(IClientRepository repository, SortingService<GetClientViewModel> sortingClient)
    {
        _repository = repository;
        _sortingClient = sortingClient;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string sortOrder)
    {
        ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

        var clients = await _sortingClient.SortModel(sortOrder);
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
        if (id == null)
            return NotFound();
        
        await _repository.RemoveClient(id);
        return await Task.Run(() => RedirectToAction(nameof(Index)));
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return await Task.Run(() => View());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AddClientViewModel newClient)
    {
        if (!ModelState.IsValid)
            return View(nameof(Create));

        await _repository.AddClient(newClient);

        return await Task.Run(() => RedirectToAction(nameof(Index)));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var client = await _repository.GetClient(id);
        if (client == null)
            return NotFound();

        return await Task.Run(() => View(client));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateClientViewModel updatedClient)
    {
        if (!ModelState.IsValid)
            return View(nameof(Edit));

        await _repository.UpdateClient(updatedClient);

        return await Task.Run(() => RedirectToAction(nameof(Index)));
    }
}