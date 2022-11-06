using InventoryControl.WebUI.Attributes;
using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.ViewModels.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControl.WebUI.Controllers
{
    public class WhatsAppController : BaseController
    {
        private readonly IWhatsAppModelFactory _whatsAppModelFactory;

        public WhatsAppController(IWhatsAppModelFactory whatsAppModelFactory)
        {
            _whatsAppModelFactory = whatsAppModelFactory;
        }

        [HttpGet, SessionExpire]
        public async Task<IActionResult> Send()
        {
            var viewModel = await _whatsAppModelFactory.WhatsAppViewModel();
            return View(viewModel);
        }

        [HttpPost, SessionExpire]
        public async Task<IActionResult> Send(WhatsAppViewModel viewModel)
        {
            
            return View(viewModel);
        }
    }
}
