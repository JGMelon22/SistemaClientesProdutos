using ClientesProdutos.Interfaces;

namespace ClientesProdutos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _repository.GetProducts();
        return products != null
            ? Ok(products)
            : NoContent();
    }
}