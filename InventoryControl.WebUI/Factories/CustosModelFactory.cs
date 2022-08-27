using InventoryControl.Models.Entities;
using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.ViewModels.Custos;

namespace InventoryControl.WebUI.Factories
{
    public class CustosModelFactory : ICustosModelFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<CustosViewModel> PrepareCustosViewModel()
        {
            return Task.FromResult(new CustosViewModel 
            {
                Data = DateTime.Now.Date
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<CustosViewModel> PrepareCustosViewModel(CustosModel custoModel)
        {
            return Task.FromResult(new CustosViewModel
            {
                Id = custoModel.Id,
                Data = custoModel.Data,
                Descricao = custoModel.Descricao,
                Valor = custoModel.Valor.ToString("N2")
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<CustosModel> PrepareCustosModelDto(CustosViewModel custoViewModel)
        {
            return Task.FromResult(new CustosModel
            {
                Id = custoViewModel.Id,
                Data = custoViewModel.Data,
                Descricao = custoViewModel.Descricao,
                Valor = decimal.Parse(custoViewModel.Valor)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientes"></param>
        /// <returns></returns>
        public Task<ConsultarCustosViewModel> PrepareConsultaCustosModel(List<CustosModel> custos)
        {
            return Task.FromResult(new ConsultarCustosViewModel
            {
                DataInicio = DateTime.Now.AddMonths(-1),
                DataFim = DateTime.Now,
                Custos = custos
            });
        }
    }
}
