using AutoMapper;
using InventoryControl.Application.Interfaces;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;
using InventoryControl.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryControl.Application.Services
{
    public class CustosService : Service, ICustosService
    {
        private readonly IRepository<Custo> _custosRepository;
        public readonly IMapper _imapper;

        public CustosService(
            IRepository<Custo> custosRepository,
            IMapper imapper)
        {
            _custosRepository = custosRepository;
            _imapper = imapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<CustosModel> FindById(int Id)
        {
            return _imapper.Map<CustosModel>(await _custosRepository.FindById(Id));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CustosModel> CreateCusto(CustosModel model)
        {
            var cliente = _imapper.Map<Custo>(model);
            cliente = await _custosRepository.Insert(cliente);
            await _custosRepository.CommitAsync();
            return _imapper.Map<CustosModel>(cliente);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<CustosModel>> SearchCustos(CustosModel model, DateTime dataInicio, DateTime dataFim)
        {
            var custos = await _custosRepository.Table.Where(c => c.Data >= dataInicio && c.Data <= dataFim).ToListAsync();
            return _imapper.Map<List<CustosModel>>(custos).OrderBy(a => a.Data).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CustosModel> UpdateCusto(CustosModel model)
        {
            var cliente = _imapper.Map<Custo>(model);

            cliente = await _custosRepository.Update(cliente);
            await _custosRepository.CommitAsync();

            return _imapper.Map<CustosModel>(cliente);
        }
    }
}
