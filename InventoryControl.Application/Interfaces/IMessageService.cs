using InventoryControl.Models.Entities;

namespace InventoryControl.Application.Interfaces
{
    public interface IMessageService
    {
        Task<MessageModel> CreateMessage(MessageModel model);
        Task<List<MessageModel>> Find(int? situacao, bool SaveProcessed = true);
        Task ImportMessages(List<MessageModel> messages);
        Task IntegrateMessage(MessageModel messageModel);
        Task IntegrateMessageCliente(ClienteModel clienteModel, int idMessage);
        Task IntegrateMessageServico(ServicoModel servicoModel, int idMessage);
        Task UpdateMessageProcessada(int MessageId);
    }
}
