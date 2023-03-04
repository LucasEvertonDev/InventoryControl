using AutoMapper;
using InventoryControl.Application.Interfaces;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;
using InventoryControl.Models.Entities;
using InventoryControl.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace InventoryControl.Application.Services
{
    public class MessageService : Service, IMessageService
    {
        private readonly IRepository<Message> _messageRepository;
        private readonly IRepository<Cliente> _clienteRepository;
        private readonly IRepository<Servico> _servicoRepository;
        private readonly IRepository<Atendimento> _atendimentoRepository;
        private readonly IRepository<MapServicosAtendimento> _mapServicosAtendimentoRepository;
        private readonly IMapper _mapper;

        public MessageService(IRepository<Message> messageRepository,
            IRepository<Cliente> clienteRepository,
            IRepository<Servico> servicoRepository,
            IRepository<Atendimento> atendimentoRepository,
            IMapper mapper,
            IRepository<MapServicosAtendimento> mapServicosAtendimentoRepository)
        {
            this._messageRepository = messageRepository;
            this._clienteRepository = clienteRepository;
            this._servicoRepository = servicoRepository;
            this._atendimentoRepository = atendimentoRepository;
            this._mapServicosAtendimentoRepository = mapServicosAtendimentoRepository;
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
        public async Task<List<MessageModel>> Find(int? situacao, bool SaveProcessed = true)
        {
            var messages = await _messageRepository.Table
                .Where(a => !situacao.HasValue || a.Situacao == situacao.Value)
                .ToListAsync();

            if (SaveProcessed)
            {
                foreach (var msg in messages)
                {
                    msg.Situacao = (int)SituacaoMessage.PROCESSADA;

                    await _messageRepository.Update(msg);
                }

                await _messageRepository.CommitAsync();
            }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clienteModel"></param>
        /// <param name="idMessage"></param>
        /// <returns></returns>
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

        /// 
        /// </summary>
        /// <param name="servicoModel"></param>
        /// <returns></returns>
        public async Task IntegrateMessageAtendimento(AtendimentoModel atendimentoModel, int idMessage)
        {
            var atendimento = _mapper.Map<Atendimento>(atendimentoModel);

            var atendimentoDb = await _atendimentoRepository.Table.Where(c => c.IdExterno == atendimento.IdExterno).FirstOrDefaultAsync();

            if (atendimentoDb != null)
            {
                var clienteDb = await _clienteRepository.Table.Where(cliente => cliente.IdExterno == atendimento.ClienteIdExterno).FirstOrDefaultAsync();

                atendimentoDb.Data = atendimento.Data;
                atendimentoDb.ClienteId = clienteDb.Id;
                atendimentoDb.Cliente = clienteDb;
                atendimentoDb.ClienteIdExterno = atendimento.ClienteIdExterno;
                atendimentoDb.Situacao = atendimento.Situacao;
                atendimentoDb.ObservacaoAtendimento = atendimento.ObservacaoAtendimento;
                atendimentoDb.ValorPago = atendimento.ValorPago;
                atendimentoDb.ValorAtendimento = atendimento.ValorAtendimento;

                _atendimentoRepository.Update(atendimentoDb);

                var maps = await _mapServicosAtendimentoRepository.Table.Where(a => a.AtendimentoId == atendimentoDb.Id).ToListAsync();

                maps.ForEach(a =>
                {
                    _mapServicosAtendimentoRepository.Delete(a);
                });

                foreach (var mapServicoModel in atendimentoModel.MapServicosAtendimen)
                {
                    var servicoDb = await _servicoRepository.Table.Where(a => a.IdExterno == mapServicoModel.ServicoIdExterno).FirstOrDefaultAsync();

                    var mapServico = new MapServicosAtendimento()
                    {
                        IdExterno = mapServicoModel.IdExterno,
                        AtendimentoIdExterno = atendimentoDb.IdExterno,
                        AtendimentoId = atendimentoDb.Id,
                        ServicoId = servicoDb.Id,
                        ServicoIdExterno = servicoDb.IdExterno,
                        Atendimento = atendimentoDb,
                        Servico = servicoDb,
                        ValorCobrado = mapServicoModel.ValorCobrado,
                    };

                    await _mapServicosAtendimentoRepository.Insert(mapServico);
                }
            }
            else
            {

                var clienteDb = await _clienteRepository.Table.Where(cliente => cliente.IdExterno == atendimento.ClienteIdExterno).FirstOrDefaultAsync();

                var atendimentoToSave = new Atendimento
                {
                    Data = atendimento.Data,
                    Cliente = clienteDb,
                    ClienteId = clienteDb.Id,
                    Situacao = atendimento.Situacao,
                    ObservacaoAtendimento = atendimento.ObservacaoAtendimento,
                    ValorPago = atendimento.ValorPago,
                    ValorAtendimento = atendimento.ValorAtendimento,
                    IdExterno = atendimento.IdExterno,
                    ClienteAtrasado = atendimento.ClienteAtrasado,
                    ClienteIdExterno = atendimento.ClienteIdExterno,
                    MapServicosAtendimentos = new List<MapServicosAtendimento>()
                };

                atendimentoToSave = await _atendimentoRepository.Insert(atendimentoToSave);

                foreach (var mapServicoModel in atendimentoModel.MapServicosAtendimen)
                {
                    var servicoDb = await _servicoRepository.Table.Where(a => a.IdExterno == mapServicoModel.ServicoIdExterno).FirstOrDefaultAsync();

                    var mapServico = new MapServicosAtendimento()
                    {
                        IdExterno = mapServicoModel.IdExterno,
                        AtendimentoIdExterno = atendimentoToSave.IdExterno,
                        AtendimentoId = atendimentoToSave.Id,
                        ServicoId = servicoDb.Id,
                        ServicoIdExterno = servicoDb.IdExterno,
                        Atendimento = atendimentoToSave,
                        Servico = servicoDb,
                        ValorCobrado = mapServicoModel.ValorCobrado,
                    };

                    await _mapServicosAtendimentoRepository.Insert(mapServico);
                }
            }

            await UpdateMessageProcessada(idMessage);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public async Task IntegrateMessage(MessageModel messageModel)
        {
            try
            {
                if (messageModel.TypeMessage == (int)TypeMessage.Cliente)
                {
                    var cliente = JsonConvert.DeserializeObject<ClienteModel>(messageModel.JsonMessage);

                    await IntegrateMessageCliente(cliente, messageModel.Id.GetValueOrDefault());
                }
                else if(messageModel.TypeMessage == (int)TypeMessage.Servico)
                {
                    var servico = JsonConvert.DeserializeObject<ServicoModel>(messageModel.JsonMessage);

                    await IntegrateMessageServico(servico, messageModel.Id.GetValueOrDefault());
                }
                else if (messageModel.TypeMessage == (int)TypeMessage.Atendimento)
                {
                    var atendimentoModel = JsonConvert.DeserializeObject<AtendimentoModel>(messageModel.JsonMessage);

                    await IntegrateMessageAtendimento(atendimentoModel, messageModel.Id.GetValueOrDefault());
                }
            }
            catch (Exception ex)
            {

            }
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
