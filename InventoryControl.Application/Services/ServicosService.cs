using AutoMapper;
using InventoryControl.Application.Interfaces;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;
using InventoryControl.Models.Entities;
using Microsoft.EntityFrameworkCore;

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
            return await _servicoRepository.Table.Where(a => a.Nome.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ServicoModel> FindById(int Id)
        {
            return _imapper.Map<ServicoModel>(await _servicoRepository.FindById(Id));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ServicoModel> CreateServico(ServicoModel model)
        {
            var cliente = _imapper.Map<Servico>(model);
            if (await this.FindByName(model.Nome) != null)
            {
                LogicalException("Já existe um servico cadastrado com esse nome");
            }

            cliente = await _servicoRepository.Insert(cliente);
            await _servicoRepository.CommitAsync();
            return _imapper.Map<ServicoModel>(cliente);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ServicoModel>> SearchServicos(ServicoModel model)
        {
            var clientes = await _servicoRepository.Table.Where(a => (string.IsNullOrEmpty(model.Nome) || a.Nome == model.Nome)
                    && (string.IsNullOrEmpty(model.Descricao) || a.Descricao.ToLower().Contains(model.Descricao.ToLower()))).ToListAsync();

            return _imapper.Map<List<ServicoModel>>(clientes);
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
            await _servicoRepository.CommitAsync();

            return _imapper.Map<ServicoModel>(cliente);
        }
    }
}
