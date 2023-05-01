using ClientesProdutos.Interfaces;

namespace ClientesProdutos.Controllers;

public class ProductsController : Controller
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
            ? await Task.Run(() => View(products))
            : NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var product = await _repository.GetProduct(id);
        return product != null
            ? await Task.Run(() => View(product))
            : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _repository.GetProduct(id);
        return product != null
            ? await Task.Run(() => View(product))
            : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return await Task.Run(() => View());
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddProductViewModel newProduct)
    {
        if (!ModelState.IsValid)
            return View(nameof(Create));

        await _repository.AddProduct(newProduct);

        return await Task.Run(() => RedirectToAction(nameof(Index)));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _repository.GetProduct(id);
        if (product == null)
            return NotFound();

        return await Task.Run(() => View(product));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateProductViewModel updatedProduct)
    {
        if (!ModelState.IsValid)
            return View(nameof(Create));

        await _repository.UpdateProduct(updatedProduct);

        return await Task.Run(() => RedirectToAction(nameof(Index)));
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var productToRemove = await _repository.RemoveProduct(id);
        return productToRemove != null
            ? await Task.Run(() => RedirectToAction(nameof(System.Index)))
            : NotFound();
    }
}