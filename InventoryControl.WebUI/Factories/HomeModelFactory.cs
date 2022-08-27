using InventoryControl.Application.Interfaces;
using InventoryControl.WebUI.Enuns;
using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.ViewModels.Home;

namespace InventoryControl.WebUI.Factories
{
    public class HomeModelFactory : IHomeModelFactory
    {
        private IAtendimentoService _atendimentoService;

        public HomeModelFactory(IAtendimentoService atendimentoService)
        {
            _atendimentoService = atendimentoService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<HomeViewModel> PrepareHomeViewModel()
        {
            var rest = await _atendimentoService.SeachAgendamentos(
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01),
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01).AddMonths(1).AddSeconds(-1));

            return new HomeViewModel
            {
                Agendados = rest.Where(a => a.Situacao == (int)SituacaoAtendimento.AGENDADO).Count(),
                Concluidos = rest.Where(a => a.Situacao == (int)SituacaoAtendimento.CONCLUIDO).Count(),
                Lucro = rest.Where(a => a.Situacao == (int)SituacaoAtendimento.CONCLUIDO).Sum(a => a.ValorPago.GetValueOrDefault()),
                Receber = rest.Where(a => a.Situacao == (int)SituacaoAtendimento.CONCLUIDO).Sum(a => a.ValorAtendimento) 
                    - rest.Where(a => a.Situacao == (int)SituacaoAtendimento.CONCLUIDO).Sum(a => a.ValorPago.GetValueOrDefault())
            };
        }

    }
}
