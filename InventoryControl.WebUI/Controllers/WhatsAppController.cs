using InventoryControl.WebUI.Attributes;
using InventoryControl.WebUI.Identity.Constants;
using InventoryControl.WebUI.ViewModels.Atendimentos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhatsApp.SimpleCRM.Domain.Contracts.Service.Core;

namespace InventoryControl.WebUI.Controllers
{
    public class WhatsAppController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStartupService _startupService;

        public WhatsAppController(ILogger<HomeController> logger,
            IStartupService startupService)
        {
            _logger = logger;
            _startupService = startupService;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, SessionExpire, Authorize(Roles = Roles.VISUALIZAR_DASHBOARD)]
        public IActionResult Send()
        {
            return View(new WhatsAppViewModel());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Send(WhatsAppViewModel whatsAppViewModel)
        {
            _startupService.SendMessage( whatsAppViewModel.Message, whatsAppViewModel.Numero);

            AddSuccess("Mensagem enviada com sucesso");

            return View(new WhatsAppViewModel());
        }
    }
}
