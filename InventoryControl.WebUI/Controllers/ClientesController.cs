using AutoMapper;
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
        private readonly IMapper _imapper;

        public ClientesController(IClienteModelFactory clienteModelFactory,
            IClienteService clienteService,
            IMapper imapper)
        {
            _clienteModelFactory = clienteModelFactory;
            _clienteService = clienteService;
            _imapper = imapper;
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
                    var cliente = await _clienteService.CreateCliente(
                        await _clienteModelFactory.PrepareClienteModelDto(viewModel));

                    if (cliente.Id > 0)
                    {
                        AddSuccess("Cliente cadastrado com sucesso!");
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
        [HttpGet, SessionExpire, Authorize(Roles = Roles.MANTER_CLIENTES)]
        public async Task<IActionResult> Edit(int Id)
        {
            var viewModel = await _clienteModelFactory.PrepareClienteViewModel(await _clienteService.FindById(Id));
            return View(viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost, SessionExpire, Authorize(Roles = Roles.MANTER_CLIENTES)]
        public async Task<IActionResult> Edit(ClienteViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cliente = await _clienteService.UpdateCliente(
                        await _clienteModelFactory.PrepareClienteModelDto(viewModel));

                    if (cliente.Id > 0)
                    {
                        AddSuccess("Cliente atualizado com sucesso!");
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
