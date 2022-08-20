using AutoMapper;
using InventoryControl.Application.Interfaces;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;
using InventoryControl.Models.Entities;

namespace InventoryControl.Application.Services
{
    public class ServicosService : Service, IServicosService
    {
        private readonly IRepository<Servico> _servicoRepository;
        public readonly IMapper _imapper;

        public ServicosService(
            IRepository<Servico> servicoRepository,
            IMapper imapper)
        {
            _servicoRepository = servicoRepository;
            _imapper = imapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Servico> FindByName(string name)
        {
            var users = await _servicoRepository.FindAll();
            return users.Where(a => a.Nome.ToLower() == name.ToLower()).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ServicoModel> FindById(int Id)
        {
            var user = await _servicoRepository.FindById(Id);
            return _imapper.Map<ServicoModel>(user);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ServicoModel> CreateServico(ServicoModel model)
        {
            try
            {
                var cliente = _imapper.Map<Servico>(model);
                if (await this.FindByName(model.Nome) != null)
                {
                    LogicalException("Já existe um servico cadastrado com esse nome");
                }


                cliente = await _servicoRepository.Insert(cliente);
                await _servicoRepository.Save();
                return _imapper.Map<ServicoModel>(cliente);
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
        public async Task<List<ServicoModel>> SearchServicos(ServicoModel model)
        {
            try
            {
                var clientes = await _servicoRepository.FindAll();
                clientes = clientes
                    .Where(a => (string.IsNullOrEmpty(model.Nome) || a.Nome == model.Nome)
                        && (string.IsNullOrEmpty(model.Descricao) || a.Descricao.ToLower().Contains(model.Descricao.ToLower())))
                    .ToList();

                return _imapper.Map<List<ServicoModel>>(clientes);
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
        public async Task<ServicoModel> UpdateServico(ServicoModel model)
        {
            var cliente = _imapper.Map<Servico>(model);
            cliente = await _servicoRepository.Update(cliente);
            await _servicoRepository.Save();
            return _imapper.Map<ServicoModel>(cliente);
        }
    }
}
