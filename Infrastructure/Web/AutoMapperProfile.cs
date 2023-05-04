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
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Products![0].ProductName))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Products![0].Value))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Clients![0].Email))
            .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Clients![0].ClientName));
    }
}