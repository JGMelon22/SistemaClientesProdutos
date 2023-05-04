using ClientesProdutos.Infrastructure.Repositories;

namespace ClientesProdutos.Controllers;

public class ClientsProductsController : Controller
{
    private readonly IDbConnection _dbConnection;
    private readonly IMapper _mapper;

    public ClientsProductsController(IDbConnection dbConnection, IMapper mapper)
    {
        _dbConnection = dbConnection;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var clientProductRepository = new ClientProductRepository(_dbConnection, _mapper);
        var clientsProducts = await clientProductRepository.GetClientProductsRelation();
        return clientsProducts != null
            ? await Task.Run(() => View(clientsProducts))
            : NoContent();
    }
}