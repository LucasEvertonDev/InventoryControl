using InventoryControl.Models.Entities;
using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.ViewModels.Servicos;

namespace InventoryControl.WebUI.Factories
{
    public class ServicoModelFactory : IServicoModelFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<ServicoViewModel> PrepareServicoViewModel()
        {
            return Task.FromResult(new ServicoViewModel { });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ServicoViewModel> PrepareServicoViewModel(ServicoModel servicoModel)
        {
            return Task.FromResult(new ServicoViewModel
            {
                Id = servicoModel.Id,
                Nome = servicoModel.Nome,
                Descricao = servicoModel.Descricao
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<ServicoModel> PrepareServicoModelDto(ServicoViewModel servicoViewModel)
        {
            return Task.FromResult(new ServicoModel
            {
                Id = servicoViewModel.Id,
                Nome = servicoViewModel.Nome,
                Descricao = servicoViewModel.Descricao
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientes"></param>
        /// <returns></returns>
        public Task<ConsultarServicosViewModel> PrepareConsultaServicosModel(List<ServicoModel> servicos)
        {
            return Task.FromResult(new ConsultarServicosViewModel
            {
                Servicos = servicos
            });
        }
    }
}
