﻿using InventoryControl.Models.Entities;

namespace InventoryControl.Application.Interfaces
{
    public interface IMessageService
    {
        Task<MessageModel> CreateMessage(MessageModel model);
        Task<List<MessageModel>> Find(int? situacao);
        Task ImportMessages(List<MessageModel> messages);
        Task IntegrateMessage(MessageModel messageModel);
        Task IntegrateMessageCliente(ClienteModel clienteModel, int idMessage);
        Task IntegrateMessageServico(ServicoModel servicoModel, int idMessage);
        Task UpdateMessageProcessada(int MessageId);
    }
}