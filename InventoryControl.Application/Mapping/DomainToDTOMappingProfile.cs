using AutoMapper;
using InventoryControl.Application.Models;
using InventoryControl.Domain.Entities;
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
        }
    }
}
