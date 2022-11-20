using AutoMapper;
using InventoryControl.Application.Interfaces;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;
using InventoryControl.Models.Entities;
using InventoryControl.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InventoryControl.Application.Services
{
    public class MessageService : Service, IMessageService
    {
        private readonly IRepository<Message> _messageRepository;
        private readonly IRepository<Cliente> _clienteRepository;
        private readonly IRepository<Servico> _servicoRepository;
        private readonly IMapper _mapper;

        public MessageService(IRepository<Message> messageRepository,
            IRepository<Cliente> clienteRepository,
            IRepository<Servico> servicoRepository,
            IMapper mapper)
        {
            this._messageRepository = messageRepository;
            this._clienteRepository = clienteRepository;
            this._servicoRepository = servicoRepository;
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

        public async Task IntegrateMessageCliente(ClienteModel clienteModel, int idMessage)
        {
            var cliente = _mapper.Map<Cliente>(clienteModel);

            var clienteDb = await _clienteRepository.Table.Where(c => c.IdExterno == cliente.IdExterno).FirstOrDefaultAsync();

            if (clienteDb != null)
            {
                clienteDb.Telefone = cliente.Telefone;
                clienteDb.Nome = cliente.Nome;
                clienteDb.Cpf = cliente.Cpf;
                clienteDb.DataNascimento = cliente.DataNascimento;
                clienteDb.DataAtualizacao = cliente.DataAtualizacao;

                _clienteRepository.Update(clienteDb);
            }
            else
            {
                cliente.Id = 0;
                _clienteRepository.Insert(cliente);
            }

            await UpdateMessageProcessada(idMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="servicoModel"></param>
        /// <returns></returns>
        public async Task IntegrateMessageServico(ServicoModel servicoModel, int idMessage)
        {
            var servico = _mapper.Map<Servico>(servicoModel);

            var servicoDb = await _servicoRepository.Table.Where(c => c.IdExterno == servico.IdExterno).FirstOrDefaultAsync();

            if (servicoDb != null)
            {
                servicoDb.Nome = servico.Nome;
                servicoDb.Descricao = servico.Descricao;

                _servicoRepository.Update(servicoDb);
            }
            else
            {
                servico.Id = 0;
                _servicoRepository.Insert(servico);
            }

            await UpdateMessageProcessada(idMessage);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public Task IntegrateMessage(MessageModel messageModel)
        {
            return Task.CompletedTask;
        }

        public async Task UpdateMessageProcessada(int MessageId)
        {
            var message = await _messageRepository.FindById(MessageId);

            message.Situacao = (int)SituacaoMessage.PROCESSADA;

            _messageRepository.Update(message);

            await _messageRepository.CommitAsync();
        }
    }
}
