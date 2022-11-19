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
    public class ClienteService : Service, IClienteService
    {
        private readonly IRepository<Cliente> _clienteRepository;
        public readonly IMapper _imapper;
        private readonly IRepository<Message> _messageRepository;
        private readonly IMessageService _messageService;

        public ClienteService(
            IRepository<Cliente> clienteRepository,
            IMapper imapper,
            IRepository<Message> messageRepository,
            IMessageService messageService)
        {
            _clienteRepository = clienteRepository;
            _imapper = imapper;
            _messageRepository = messageRepository;
            this._messageService = messageService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Cliente> FindByCpf(string cpf)
        {
            return await _clienteRepository.Table.Where(a => a.Cpf == cpf).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ClienteModel> FindById(int Id)
        {
            var user = await _clienteRepository.FindById(Id);
            return _imapper.Map<ClienteModel>(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ClienteModel> CreateCliente(ClienteModel model)
        {
            var cliente = _imapper.Map<Cliente>(model);
            if (!string.IsNullOrEmpty(cliente.Cpf) && await this.FindByCpf(model.Cpf) != null)
            {
                LogicalException("Já existe um cadastro de cliente com o cpf informado");
            }
            cliente.IdExterno = Guid.NewGuid().ToString();
            cliente = await _clienteRepository.Insert(cliente);

            _messageService.CreateMessage(new MessageModel
            {
                JsonMessage = JsonConvert.SerializeObject(cliente),
                Situacao = (int)SituacaoMessage.AGUARDANDO_PROCESSAMENTO_MOBILE,
                TypeMessage = (int)TypeMessage.Cliente
            });

            await _clienteRepository.CommitAsync();
            return _imapper.Map<ClienteModel>(cliente);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ClienteModel>> SearchClientes(ClienteModel model)
        {
            var clientes = await _clienteRepository.Table.Where(a => (string.IsNullOrEmpty(model.Cpf) || a.Cpf == model.Cpf)
                && (string.IsNullOrEmpty(model.Nome) || a.Nome.ToLower().Contains(model.Nome.ToLower()))).ToListAsync();
            return _imapper.Map<List<ClienteModel>>(clientes).OrderBy(a => a.Nome).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ClienteModel> UpdateCliente(ClienteModel model)
        {
            var cliente = _imapper.Map<Cliente>(model);
            cliente = await _clienteRepository.Update(cliente);
            await _clienteRepository.CommitAsync();
            return _imapper.Map<ClienteModel>(cliente);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task UpdateCarga()
        {
            try
            {
                var clientes = await _clienteRepository.Table.ToListAsync();
                foreach (var cliente in clientes)
                {
                    cliente.IdExterno = Guid.NewGuid().ToString();
                    _clienteRepository.Update(cliente);
                    _messageRepository.Insert(new Message
                    {
                        JsonMessage = JsonConvert.SerializeObject(cliente),
                        Situacao = (int)SituacaoMessage.AGUARDANDO_PROCESSAMENTO_MOBILE,
                        TypeMessage = (int)TypeMessage.Cliente
                    });
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                await _clienteRepository.CommitAsync();
            }
        }
    }
}
