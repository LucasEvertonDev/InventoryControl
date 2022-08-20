using InventoryControl.Application.Interfaces;
using InventoryControl.Models.Entities;
using InventoryControl.WebUI.Attributes;
using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.Identity.Constants;
using InventoryControl.WebUI.ViewModels.Atendimentos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace InventoryControl.WebUI.Controllers
{
    public class AtendimentosController : BaseController
    {
        private readonly IServicosService _servicosService;
        private readonly IAtendimentoModelFactory _atendimentoModelFactory;

        public AtendimentosController(IAtendimentoModelFactory atendimentoModelFactory,
            IServicosService servicosService)
        {
            _atendimentoModelFactory = atendimentoModelFactory;
            _servicosService = servicosService;
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
        /// <param name="posicao"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddDetalhamento(int posicao)
        {
            return PartialView("../Atendimentos/_ServicoDetalhes", this.NewDetalhamento(posicao));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="posicao"></param>
        /// <returns></returns>
        private AssociacaoServicoAtendimentoViewModel NewDetalhamento(int posicao = 0)
        {
            var servicos = _servicosService.SearchServicos(new Models.Entities.ServicoModel() { });
            var id = posicao + 1;
            return new ViewModels.Atendimentos.AssociacaoServicoAtendimentoViewModel
            {
                PosicaoLista = posicao,
                Apagado = false,
                ComboServicos = servicos.Result.Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString() }).ToList(),
            };
        }


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="viewModel"></param>
        ///// <returns></returns>
        //[HttpPost, SessionExpire, Authorize(Roles = Roles.MANTER_SERVICOS)]
        //public async Task<IActionResult> Create(ServicoViewModel viewModel)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var cliente = await _servicoService.CreateServico(
        //                await _servicoModelFactory.PrepareServicoModelDto(viewModel));

        //            if (cliente.Id > 0)
        //            {
        //                AddSuccess("Servico cadastrado com sucesso!");
        //                viewModel.Enabled = false;
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        TratarException(e);
        //    }
        //    return View(viewModel);
        //}
    }
}
