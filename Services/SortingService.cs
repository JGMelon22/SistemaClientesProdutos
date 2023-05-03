namespace ClientesProdutos.Services;

public abstract class SortingService<T>
{
    public abstract Task<List<T>> SortModel(string sortOrder);
}