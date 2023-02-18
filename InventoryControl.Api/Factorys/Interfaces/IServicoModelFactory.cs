using InventoryControl.Models.DTOs;
using InventoryControl.Models.Entities;

namespace InventoryControl.Api.Factorys.Interfaces
{
    public interface IServicoModelFactory
    {
        ServicoModel ConvertDtoToModel(ServicoDTO servicoDTO);
        List<ServicoModel> ConvertListDtoToListModel(List<ServicoDTO> servicoDtos);
        List<ServicoDTO> ConvertListModelToListDto(List<ServicoModel> servicoModels);
        ServicoDTO ConvertModelToDto(ServicoModel servicoModel);
    }
}