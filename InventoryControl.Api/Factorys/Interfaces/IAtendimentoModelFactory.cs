using InventoryControl.Models.Dto;
using InventoryControl.Models.Entities;

namespace InventoryControl.Api.Factorys.Interfaces
{
    public interface IAtendimentoModelFactory
    {
        AtendimentoDTO ConvertModelToDto(AtendimentoModel atendimentoModel);
    }
}
