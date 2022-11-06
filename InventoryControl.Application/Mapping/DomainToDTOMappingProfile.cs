using AutoMapper;
using InventoryControl.Domain.Entities;
using InventoryControl.Models.Entities;

namespace InventoryControl.Application.Mapping
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Usuario, UsuarioModel>().ReverseMap();
            CreateMap<Cliente, ClienteModel>().ReverseMap();
            CreateMap<Servico, ServicoModel>().ReverseMap();
            CreateMap<Atendimento, AtendimentoModel>().ReverseMap();
            CreateMap<AssociacaoServicosAtendimentoModel, MapServicosAtendimento>().ReverseMap();
            CreateMap<Custo, CustosModel>().ReverseMap();
            CreateMap<Message, MessageModel>().ReverseMap();
        }
    }
}
