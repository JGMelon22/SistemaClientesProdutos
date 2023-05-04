using ClientesProdutos.ViewModels.ClientProduct;

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
        
        // Join table
        CreateMap<ClientProduct, GetClientProductViewModel>()
            .ForMember(dest=> dest.Email, opt => opt.MapFrom(src=> src.Clients![0].Email))
            .ForMember(dest=> dest.Name, opt => opt.MapFrom(src=> src.Clients![0].Name))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Products![0].Value));
    }
}
