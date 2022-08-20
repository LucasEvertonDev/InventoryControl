using AutoMapper;
using InventoryControl.Domain.Entities;
using InventoryControl.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Application.Mapping
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Usuario, UsuarioModel>().ReverseMap();
            CreateMap<Cliente, ClienteModel>().ReverseMap();
            CreateMap<Servico, ServicoModel>().ReverseMap();
        }
    }
}
