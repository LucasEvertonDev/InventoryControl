using AutoMapper;
using InventoryControl.Application.Interfaces;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;
using InventoryControl.Models.Entities;
using InventoryControl.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace InventoryControl.Application.Services
{
    public class MessageService : Service, IMessageService
    {
        private readonly IRepository<Message> _messageRepository;
        private readonly IMapper _mapper;

        public MessageService(IRepository<Message> messageRepository,
            IMapper mapper)
        {
            this._messageRepository = messageRepository;
            this._mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MessageModel> CreateMessage(MessageModel model)
        {
            var message = _mapper.Map<Message>(model);

            message = await _messageRepository.Insert(message);
            await _messageRepository.CommitAsync();

            return _mapper.Map<MessageModel>(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<MessageModel>> Find(int? situacao)
        {
            var messages = await _messageRepository.Table
                .Where(a => !situacao.HasValue || a.Situacao == situacao.Value)
                .ToListAsync();

            foreach (var msg in messages)
            {
                msg.Situacao = (int)SituacaoMessage.PROCESSADA;
                _messageRepository.Update(msg);
            }

            await _messageRepository.CommitAsync();

            return _mapper.Map<List<MessageModel>>(messages);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public async Task ImportMessages(List<MessageModel> messages)
        {
            foreach(var msgModel in messages)
            {
                var message = _mapper.Map<Message>(msgModel);
                message = await _messageRepository.Insert(message);
            }

            await _messageRepository.CommitAsync();

        }
    }
}
