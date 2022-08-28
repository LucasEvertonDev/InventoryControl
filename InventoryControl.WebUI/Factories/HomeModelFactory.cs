using InventoryControl.Application.Interfaces;
using InventoryControl.WebUI.Enuns;
using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.ViewModels.Charts;
using InventoryControl.WebUI.ViewModels.Home;

namespace InventoryControl.WebUI.Factories
{
    public class HomeModelFactory : IHomeModelFactory
    {
        private IAtendimentoService _atendimentoService;
        private ICustosService _custosService;

        public HomeModelFactory(IAtendimentoService atendimentoService,
            ICustosService custosService)
        {
            _atendimentoService = atendimentoService;
            _custosService = custosService;
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

            var custos = await _custosService.SearchCustos(new Models.Entities.CustosModel { }, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01),
                 new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01).AddMonths(1).AddSeconds(-1));

            return new HomeViewModel
            {
                Agendados = rest.Where(a => a.Situacao == (int)SituacaoAtendimento.AGENDADO).Count(),
                Concluidos = rest.Where(a => a.Situacao == (int)SituacaoAtendimento.CONCLUIDO).Count(),
                Lucro = rest.Where(a => a.Situacao == (int)SituacaoAtendimento.CONCLUIDO).Sum(a => a.ValorPago.GetValueOrDefault()) 
                    - custos.Sum(a => a.Valor),
                Receber = rest.Where(a => a.Situacao == (int)SituacaoAtendimento.CONCLUIDO).Sum(a => a.ValorAtendimento) 
                    - rest.Where(a => a.Situacao == (int)SituacaoAtendimento.CONCLUIDO).Sum(a => a.ValorPago.GetValueOrDefault())
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Graph3SeriesViewModel> PrepareGraphViewModel()
        {
            var data = DateTime.Now.AddMonths(-12);
            var graph = new Graph3SeriesViewModel();
            var atendimentos = await _atendimentoService.SeachAgendamentos(
                new DateTime(data.Year, data.Month, 01),
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01).AddMonths(1).AddSeconds(-1));

            var custos = await _custosService.SearchCustos(new Models.Entities.CustosModel { },
                new DateTime(data.Year, data.Month, 01),
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01).AddMonths(1).AddSeconds(-1));

            atendimentos = atendimentos.OrderBy(a => a.Data).ToList();
            custos = custos.OrderBy(a => a.Data).ToList();

            var x = atendimentos.GroupBy(a => new { a.Data.Month, a.Data.Year });

            var k = custos.GroupBy(a => new { a.Data.Month, a.Data.Year });

            foreach (var i in x)
            {
                graph.Serie1.SerieName = "Ganhos";
                graph.Serie1.Labels.Add(i.Key.Month.ToString("00") + "/" + i.Key.Year.ToString());
                graph.Serie1.Dados.Add(new DataDados
                {
                    y = (double)i.ToList().Sum(a => a.ValorPago.GetValueOrDefault())
                }); 
            }

            foreach (var i in x)
            {
                var g = k.Where(a => a.Key.Month == i.Key.Month && a.Key.Year == i.Key.Year).ToList().Select(a => a.ToList());
                decimal ss = 0;
                foreach (var r in g)
                {
                    ss = r.ToList().Sum(a => a.Valor);
                    break;
                }

                graph.Serie2.SerieName = "Custos";
                graph.Serie2.Labels.Add(i.Key.Month.ToString("00") + "-" + i.Key.Year.ToString());
                graph.Serie2.Dados.Add(new DataDados
                {
                    y = (double)ss
                });
            }

            foreach (var i in x)
            {
                var g = k.Where(a => a.Key.Month == i.Key.Month && a.Key.Year == i.Key.Year).ToList().Select(a => a.ToList());
                decimal ss = 0;
                foreach (var r in g)
                {
                    ss = r.ToList().Sum(a => a.Valor);
                    break;
                }

                graph.Serie3.SerieName = "Lucro";
                graph.Serie3.Labels.Add(i.Key.Month.ToString("00") + "/" + i.Key.Year.ToString());
                graph.Serie3.Dados.Add(new DataDados
                {
                    y = (double)(i.ToList().Sum(a => a.ValorPago.GetValueOrDefault()) - ss)
                });
            }

            return graph;
        }
    }
}
