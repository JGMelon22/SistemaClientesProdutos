using ClientesProdutos.Infrastructure.Repositories;

namespace ClientesProdutos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsProductsController : ControllerBase
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
        var result = await clientProductRepository.GetClientProductsRelation();
        return result != null
            ? Ok(result)
            : NoContent();
    }
}