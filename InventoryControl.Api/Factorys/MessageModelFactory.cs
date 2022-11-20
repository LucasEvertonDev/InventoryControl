using InventoryControl.Api.Factorys.Interfaces;
using InventoryControl.Models.DTOs;
using InventoryControl.Models.Entities;

namespace InventoryControl.Api.Factorys
{
    public class MessageModelFactory : IMessageModelFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageDto"></param>
        /// <returns></returns>
        public MessageModel ConvertDtoToModel(MessageDTO messageDto)
        {
            return new MessageModel()
            {
                Id = messageDto.Id,
                JsonMessage = messageDto.JsonMessage,
                Situacao = messageDto.Situacao,
                TypeMessage = messageDto.TypeMessage
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public MessageDTO ConvertModelToDto(MessageModel messageModel)
        {
            return new MessageDTO()
            {
                Id = messageModel.Id,
                JsonMessage = messageModel.JsonMessage,
                Situacao = messageModel.Situacao,
                TypeMessage = messageModel.TypeMessage
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageDTOs"></param>
        /// <returns></returns>
        public List<MessageModel> ConvertListDtoToListModel(List<MessageDTO> messageDTOs)
        {
            var messageModels = new List<MessageModel>();
            if (messageDTOs != null && messageDTOs.Count > 0)
            {
                messageDTOs.ForEach(messageDto =>
                {
                    messageModels.Add(this.ConvertDtoToModel(messageDto));
                });
            }
            return messageModels;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageDTOs"></param>
        /// <returns></returns>
        public List<MessageDTO> ConvertListModelToListDto(List<MessageModel> messageModels)
        {
            var messageDtos = new List<MessageDTO>();
            if (messageModels != null && messageModels.Count > 0)
            {
                messageModels.ForEach(messageModel =>
                {
                    messageDtos.Add(this.ConvertModelToDto(messageModel));
                });
            }
            return messageDtos;
        }
    }
}
