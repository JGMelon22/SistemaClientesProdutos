using ClientesProdutos.Domain.Entities;
using ClientesProdutos.ViewModels.Client;

namespace ClientesProdutos.Infrastructure.Web;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Client, GetClientViewModel>();
        CreateMap<AddClientViewModel, Client>();
        CreateMap<UpdateClientViewModel, Client>();
    }
}