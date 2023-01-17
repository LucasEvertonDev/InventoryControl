using AutoMapper;
using AWASP.WebUI.Data.Domains;
using AWASP.WebUI.ViewModels;

namespace AWASP.WebUI.Mapping
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
        }
    }
}
