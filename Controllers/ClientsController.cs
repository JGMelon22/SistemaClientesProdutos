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
    public async 
}