using InventoryControl.WebUI.Attributes;
using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.Identity.Constants;
using InventoryControl.WebUI.ViewModels;
using InventoryControl.WebUI.ViewModels.Clientes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControl.WebUI.Controllers
{
    [SessionExpire, Authorize(Roles = Roles.VISUALIZAR_DASHBOARD)]
    public class ClientesController : Controller
    {
        private readonly IClienteModelFactory _clienteModelFactory;

        public ClientesController(IClienteModelFactory clienteModelFactory)
        {
            _clienteModelFactory = clienteModelFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, SessionExpire, Authorize(Roles = Roles.VISUALIZAR_DASHBOARD)]
        public async Task<IActionResult> Create()
        {
            return View(await _clienteModelFactory.PrepareClienteViewModel());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost, SessionExpire, Authorize(Roles = Roles.VISUALIZAR_DASHBOARD)]
        public async Task<IActionResult> Create(ClienteViewModel viewModel)
        {
            return View(await _clienteModelFactory.PrepareClienteViewModel());
        }
    }
}
