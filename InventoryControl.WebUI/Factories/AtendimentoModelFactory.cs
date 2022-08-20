using InventoryControl.Application.Interfaces;
using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.ViewModels.Atendimentos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryControl.WebUI.Factories
{
    public class AtendimentoModelFactory : IAtendimentoModelFactory
    {
        private IServicosService _servicosService;
        private readonly IClienteService _clienteService;

        public AtendimentoModelFactory(IServicosService servicosService,
            IClienteService clienteService)
        {
            _servicosService = servicosService;
            _clienteService = clienteService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<AtendimentoViewModel> PrepareAtendimentoViewModel()
        {
            var atendimento = new AtendimentoViewModel();
            var servicos = await _servicosService.SearchServicos(new Models.Entities.ServicoModel() { });
            var clientes = await _clienteService.SearchClientes(new Models.Entities.ClienteModel() { });

            return new AtendimentoViewModel
            {
                ComboClientes = clientes.Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString() }).ToList(),
                Servicos = new List<Models.Entities.ServicoModel>(),
                Cliente = new Models.Entities.ClienteModel()
            };
        }
    }
}
