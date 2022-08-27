using AutoMapper;
using InventoryControl.Application.Interfaces;
using InventoryControl.Models;
using InventoryControl.Models.Entities;
using InventoryControl.WebUI.Attributes;
using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.Identity.Constants;
using InventoryControl.WebUI.ViewModels.Custos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControl.WebUI.Controllers
{
    public class CustosController : BaseController
    {
        private readonly ICustosModelFactory _custosModelFactory;
        private readonly ICustosService _custosService;
        private readonly IMapper _imapper;

        public CustosController(ICustosModelFactory servicosModelFactory,
            ICustosService custosService,
            IMapper imapper)
        {
            _custosModelFactory = servicosModelFactory;
            _custosService = custosService;
            _imapper = imapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, SessionExpire, Authorize(Roles = Roles.MANTER_CUSTOS)]
        public async Task<IActionResult> Index()
        {
            var viewModel = await _custosModelFactory.PrepareConsultaCustosModel(
                    await _custosService.SearchCustos(new CustosModel() { }, DateTime.Now.AddMonths(-1).Date, DateTime.Now.Date));
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost, SessionExpire, Authorize(Roles = Roles.MANTER_CUSTOS)]
        public async Task<IActionResult> Index(ConsultarCustosViewModel viewModel)
        {
            var clientes = await _custosModelFactory.PrepareConsultaCustosModel(
                await _custosService.SearchCustos(new CustosModel { }, viewModel.DataInicio, viewModel.DataFim));

            return View(clientes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, SessionExpire, Authorize(Roles = Roles.MANTER_CUSTOS)]
        public async Task<IActionResult> Create()
        {
            return View(await _custosModelFactory.PrepareCustosViewModel());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost, SessionExpire, Authorize(Roles = Roles.MANTER_CUSTOS)]
        public async Task<IActionResult> Create(CustosViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cliente = await _custosService.CreateCusto(
                        await _custosModelFactory.PrepareCustosModelDto(viewModel));

                    if (cliente.Id > 0)
                    {
                        AddSuccess("Custo cadastrado com sucesso!");
                        viewModel.Enabled = false;
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
        [HttpGet, SessionExpire, Authorize(Roles = Roles.MANTER_CUSTOS)]
        public async Task<IActionResult> Edit(int Id)
        {
            var viewModel = await _custosModelFactory.PrepareCustosViewModel(await _custosService.FindById(Id));
            return View(viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost, SessionExpire, Authorize(Roles = Roles.MANTER_CUSTOS)]
        public async Task<IActionResult> Edit(CustosViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cliente = await _custosService.UpdateCusto(
                        await _custosModelFactory.PrepareCustosModelDto(viewModel));

                    if (cliente.Id > 0)
                    {
                        AddSuccess("Custo atualizado com sucesso!");
                        viewModel.Enabled = false;
                    }
                }
            }
            catch (Exception e)
            {
                TratarException(e);
            }
            return View(viewModel);
        }
    }
}
