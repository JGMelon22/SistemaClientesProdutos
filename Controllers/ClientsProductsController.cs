using ClientesProdutos.Infrastructure.Repositories;
using ClientesProdutos.Services;
using ClientesProdutos.ViewModels.ClientProduct;

namespace ClientesProdutos.Controllers;

public class ClientsProductsController : Controller
{
    // private readonly IDbConnection _dbConnection;
    private readonly SortingService<GetClientProductViewModel> _sortingClientProduct;
    private readonly IMapper _mapper;

    public ClientsProductsController(SortingService<GetClientProductViewModel> sortingClientProduct, IMapper mapper)
    {
        _sortingClientProduct = sortingClientProduct;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string sortOrder)
    {
        // var clientProductRepository = new ClientProductRepository(_dbConnection, _mapper);
        ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

        var clientsProducts = await _sortingClientProduct.SortModel(sortOrder);
        return clientsProducts != null
            ? await Task.Run(() => View(clientsProducts))
            : NoContent();
    }
}