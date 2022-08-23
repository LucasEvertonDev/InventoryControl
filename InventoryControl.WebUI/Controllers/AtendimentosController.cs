using InventoryControl.Application.Interfaces;
using InventoryControl.Models.Entities;
using InventoryControl.WebUI.Attributes;
using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.Identity.Constants;
using InventoryControl.WebUI.ViewModels.Atendimentos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace InventoryControl.WebUI.Controllers
{
    public class AtendimentosController : BaseController
    {
        private readonly IServicosService _servicosService;
        private readonly IClienteService _clienteService;
        private readonly IAtendimentoService _atendimentoService;
        private readonly IAtendimentoModelFactory _atendimentoModelFactory;

        public AtendimentosController(IAtendimentoModelFactory atendimentoModelFactory,
            IServicosService servicosService,
            IClienteService clienteService,
            IAtendimentoService atendimentoService)
        {
            _atendimentoModelFactory = atendimentoModelFactory;
            _servicosService = servicosService;
            _clienteService = clienteService;
            _atendimentoService = atendimentoService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, SessionExpire, Authorize(Roles = Roles.MANTER_ATENDIMENTOS)]
        public async Task<IActionResult> Index()
        {
            var viewModel = await _atendimentoModelFactory.PrepareConsultaAtendimentoViewModel(
                    await _atendimentoService.SeachAgendamentos(new AtendimentoModel() { }));
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost, SessionExpire, Authorize(Roles = Roles.MANTER_ATENDIMENTOS)]
        public async Task<IActionResult> Index(ConsultarAtendimentoViewModel viewModel)
        {
            var clientes = await _atendimentoModelFactory.PrepareConsultaAtendimentoViewModel(
                await _atendimentoService.SeachAgendamentos(new AtendimentoModel
                {
                    ClienteId = string.IsNullOrEmpty(viewModel.ClienteId) ? -1 : int.Parse(viewModel.ClienteId),
                    Data = viewModel.Data,
                    Situacao = (int)viewModel.SituacaoAtendimento
                }));
            return View(clientes);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, SessionExpire, Authorize(Roles = Roles.MANTER_ATENDIMENTOS)]
        public async Task<IActionResult> Create()
        {
            return View(await _atendimentoModelFactory.PrepareAtendimentoViewModel());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost, SessionExpire, Authorize(Roles = Roles.MANTER_SERVICOS)]
        public async Task<IActionResult> Create(AtendimentoViewModel viewModel)
        {
            try
            {
                viewModel = await _atendimentoModelFactory.PrepareAtendimentoViewModel(viewModel);

                if (ModelState.IsValid)
                {
                    if (!viewModel.ServicosAssociados.Any(a => !a.Apagado))
                    {
                        AddError("Adicione pelo menos um serviço");
                        return View(viewModel);
                    }

                    var atendimento = await _atendimentoService.CreateAtendimento(
                        await _atendimentoModelFactory.PrepareAtendimentoModelDto(viewModel));

                    if (atendimento.Id > 0)
                    {
                        AddSuccess("Atendimento cadastrado com sucesso");
                    }
                }
            }
            catch (Exception e)
            {
                TratarException(e);
            }

            return View(viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, SessionExpire, Authorize(Roles = Roles.MANTER_ATENDIMENTOS)]
        public async Task<IActionResult> Edit(int Id)
        {
            return View(await _atendimentoModelFactory.PrepareAtendimentoViewModel(
                await _atendimentoService.FindById(Id)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost, SessionExpire, Authorize(Roles = Roles.MANTER_CLIENTES)]
        public async Task<IActionResult> Edit(AtendimentoViewModel viewModel)
        {
            try
            {
                viewModel = await _atendimentoModelFactory.PrepareAtendimentoViewModel(viewModel);

                if (ModelState.IsValid)
                {
                    if (!viewModel.ServicosAssociados.Any(a => !a.Apagado))
                    {
                        AddError("Adicione pelo menos um serviço");
                        return View(viewModel);
                    }

                    var atendimento = await _atendimentoService.UpdateAtendimento(
                        await _atendimentoModelFactory.PrepareAtendimentoModelDto(viewModel));

                    if (atendimento.Id > 0)
                    {
                        AddSuccess("Atendimento atualizado com sucesso");
                    }
                }
            }
            catch (Exception e)
            {
                TratarException(e);
            }
            return View(viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetEvents(string start, string end)
        {
            var inicio = DateTime.Parse(start);
            var fim = DateTime.Parse(end);

            var events = await _atendimentoModelFactory.PrepareCalendaViewModel(
                await _atendimentoService.SeachAgendamentos(inicio, fim));
            return Json(events);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="posicao"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddDetalhamento(int posicao)
        {
            return PartialView("../Atendimentos/_ServicoDetalhes", await this.NewDetalhamento(posicao));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="posicao"></param>
        /// <returns></returns>
        private Task<AssociacaoServicoAtendimentoViewModel> NewDetalhamento(int posicao = 0)
        {
            return Task.FromResult(new ViewModels.Atendimentos.AssociacaoServicoAtendimentoViewModel
            {
                PosicaoLista = posicao,
                Apagado = false,
                ComboServicos = RecuperaServicos(),
            });
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> RecuperaServicos()
        {
            var servicos = _servicosService.SearchServicos(new Models.Entities.ServicoModel() { });
            return servicos.Result.Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString() }).ToList();
        }
    }
}
