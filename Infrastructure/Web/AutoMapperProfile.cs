namespace ClientesProdutos.Infrastructure.Web;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Client, GetClientViewModel>();
        CreateMap<AddClientViewModel, Client>();
        CreateMap<UpdateClientViewModel, Client>();
        CreateMap<Product, GetProductViewModel>();
        CreateMap<AddProductViewModel, Product>();
        CreateMap<UpdateProductViewModel, Product>();
    }
}