namespace ClientesProdutos.Interfaces;

public interface ISortingClientService
{
    Task<List<GetClientViewModel>> SortClient(string sortOrder);
}