using AutoMapper;
using InventoryControl.Application.Interfaces;
using InventoryControl.Models.Entities;
using InventoryControl.WebUI.Attributes;
using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.Identity.Constants;
using InventoryControl.WebUI.ViewModels.Clientes;
using InventoryControl.WebUI.ViewModels.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControl.WebUI.Controllers
{
    public class ServicosController : BaseController
    {
        private readonly IServicoModelFactory _servicoModelFactory;
        private readonly IServicosService _servicoService;
        private readonly IMapper _imapper;

        public ServicosController(IServicoModelFactory servicosModelFactory,
            IServicosService servicoService,
            IMapper imapper)
        {
            _servicoModelFactory = servicosModelFactory;
            _servicoService = servicoService;
            _imapper = imapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, SessionExpire, Authorize(Roles = Roles.MANTER_SERVICOS)]
        public async Task<IActionResult> Index()
        {
            var viewModel = await _servicoModelFactory.PrepareConsultaServicosModel(
                    await _servicoService.SearchServicos(new ServicoModel() { }));
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost, SessionExpire, Authorize(Roles = Roles.MANTER_SERVICOS)]
        public async Task<IActionResult> Index(ConsultarServicosViewModel viewModel)
        {
            var clientes = await _servicoModelFactory.PrepareConsultaServicosModel(
                await _servicoService.SearchServicos(new ServicoModel
                {
                    Nome = viewModel.Nome,
                    Descricao = viewModel.Descricao
                }));
            return View(clientes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, SessionExpire, Authorize(Roles = Roles.MANTER_SERVICOS)]
        public async Task<IActionResult> Create()
        {
            return View(await _servicoModelFactory.PrepareServicoViewModel());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost, SessionExpire, Authorize(Roles = Roles.MANTER_SERVICOS)]
        public async Task<IActionResult> Create(ServicoViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cliente = await _servicoService.CreateServico(
                        await _servicoModelFactory.PrepareServicoModelDto(viewModel));

                    if (cliente.Id > 0)
                    {
                        AddSuccess("Servico cadastrado com sucesso!");
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
        [HttpGet, SessionExpire, Authorize(Roles = Roles.MANTER_SERVICOS)]
        public async Task<IActionResult> Edit(int Id)
        {
            var viewModel = await _servicoModelFactory.PrepareServicoViewModel(await _servicoService.FindById(Id));
            return View(viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost, SessionExpire, Authorize(Roles = Roles.MANTER_CLIENTES)]
        public async Task<IActionResult> Edit(ServicoViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cliente = await _servicoService.UpdateServico(
                        await _servicoModelFactory.PrepareServicoModelDto(viewModel));

                    if (cliente.Id > 0)
                    {
                        AddSuccess("Servico atualizado com sucesso!");
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
