using AutoMapper;
using InventoryControl.Application.Interfaces;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;
using InventoryControl.Models.Entities;

namespace InventoryControl.Application.Services
{
    public class ClienteService : Service, IClienteService
    {
        private readonly IRepository<Cliente> _clienteRepository;
        public readonly IMapper _imapper;

        public ClienteService(
            IRepository<Cliente> clienteRepository,
            IMapper imapper)
        {
            _clienteRepository = clienteRepository;
            _imapper = imapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Cliente> FindByCpf(string cpf)
        {
            var users = await _clienteRepository.FindAll();
            return users.Where(a => a.Cpf == cpf).FirstOrDefault();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ClienteModel> CreateCliente(ClienteModel model)
        {
            try
            {
                var cliente = _imapper.Map<Cliente>(model);
                if (await this.FindByCpf(model.Cpf) != null)
                {
                    LogicalException("Já existe um cadastro de cliente com o cpf informado");
                }

                var itens = await _clienteRepository.Itens;
                if (itens.Where(u => u.Telefone == model.Telefone).Any())
                {
                    LogicalException("Já existe um cliente registrado para onúmero de telefone informado");
                }

                cliente = await _clienteRepository.Insert(cliente);
                await _clienteRepository.Save();
                return _imapper.Map<ClienteModel>(cliente);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ClienteModel>> SearchClientes(ClienteModel model)
        {
            try
            {
                var clientes = await _clienteRepository.FindAll();
                clientes = clientes
                    .Where(a => (string.IsNullOrEmpty(model.Cpf) || a.Cpf == model.Cpf)
                        && (string.IsNullOrEmpty(model.Nome) || a.Nome.ToLower().Contains(model.Nome.ToLower())))
                    .ToList();

                return _imapper.Map<List<ClienteModel>>(clientes);
            }
            catch
            {
                throw;
            }
        }
    }
}
