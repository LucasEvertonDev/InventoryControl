using InventoryControl.Models.Dto;
using InventoryControl.Models.Entities;

namespace InventoryControl.Api.Factorys.Interfaces
{
    public interface IClienteModelFactory
    {
        ClienteModel ConvertDtoToModel(ClienteDTO clienteDTO);
        List<ClienteModel> ConvertListDtoToListModel(List<ClienteDTO> clienteDTOs);
        List<ClienteDTO> ConvertListModelToListDto(List<ClienteModel> clienteModels);
        ClienteDTO ConvertModelToDto(ClienteModel clienteModel);
    }
}