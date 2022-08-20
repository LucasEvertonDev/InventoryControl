using InventoryControl.Application.Interfaces;
using InventoryControl.Models.Entities;
using InventoryControl.WebUI.Attributes;
using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.Identity.Constants;
using InventoryControl.WebUI.ViewModels;
using InventoryControl.WebUI.ViewModels.Clientes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControl.WebUI.Controllers
{
    [SessionExpire, Authorize(Roles = Roles.MANTER_CLIENTES)]
    public class ClientesController : BaseController
    {
        private readonly IClienteModelFactory _clienteModelFactory;
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteModelFactory clienteModelFactory,
            IClienteService clienteService)
        {
            _clienteModelFactory = clienteModelFactory;
            _clienteService = clienteService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, SessionExpire, Authorize(Roles = Roles.MANTER_CLIENTES)]
        public async Task<IActionResult> Index()
        {
            var viewModel = await _clienteModelFactory.PrepareConsultaClientesModel(
                    await _clienteService.SearchClientes(new ClienteModel() { }));
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost, SessionExpire, Authorize(Roles = Roles.MANTER_CLIENTES)]
        public async Task<IActionResult> Index(ConsultarClientesViewModel viewModel)
        {
            var clientes = await _clienteModelFactory.PrepareConsultaClientesModel(
                await _clienteService.SearchClientes(new ClienteModel
                {
                    Cpf = viewModel.Cpf,
                    Nome = viewModel.Nome
                })) ;
            return View(clientes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, SessionExpire, Authorize(Roles = Roles.MANTER_CLIENTES)]
        public async Task<IActionResult> Create()
        {
            return View(await _clienteModelFactory.PrepareClienteViewModel());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost, SessionExpire, Authorize(Roles = Roles.MANTER_CLIENTES)]
        public async Task<IActionResult> Create(ClienteViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = await _clienteService.CreateCliente(
                        await _clienteModelFactory.PrepareClienteModelDto(viewModel));

                    if (usuario.Id > 0)
                    {
                        AddSuccess("Cliente cadastrado com sucesso!");
                        return RedirectToAction("Login", "Account");
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
