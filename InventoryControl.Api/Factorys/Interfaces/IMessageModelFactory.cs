using InventoryControl.Models.DTOs;
using InventoryControl.Models.Entities;

namespace InventoryControl.Api.Factorys.Interfaces
{
    public interface IMessageModelFactory
    {
        MessageModel ConvertDtoToModel(MessageDTO messageDto);
        List<MessageModel> ConvertListDtoToListModel(List<MessageDTO> messageDTOs);
        List<MessageDTO> ConvertListModelToListDto(List<MessageModel> messageModels);
        MessageDTO ConvertModelToDto(MessageModel messageModel);
    }
}