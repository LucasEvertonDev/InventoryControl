using InventoryControl.WebUI.Attributes;
using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.Identity.Constants;
using InventoryControl.WebUI.ViewModels.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WhatsApp.SimpleCRM.Domain.Contracts.Service.Core;

namespace InventoryControl.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStartupService _startupService;
        private readonly IHomeModelFactory _homeModelFactory;

        public HomeController(ILogger<HomeController> logger,
            IStartupService startupService,
            IHomeModelFactory homeModelFactory)
        {
            _logger = logger;
            _startupService = startupService;
            _homeModelFactory = homeModelFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, SessionExpire, Authorize(Roles = Roles.VISUALIZAR_DASHBOARD)]
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { });
            }
            return View(await _homeModelFactory.PrepareHomeViewModel());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [HttpGet, SessionExpire, Authorize(Roles = Roles.VISUALIZAR_DASHBOARD)]
        public async Task<IActionResult> GetGraphGanhoXCustos()
        {
            var events = await _homeModelFactory.PrepareGraphViewModel();
            return Json(JsonConvert.SerializeObject(events));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            _startupService.SendMessage("message", "Pedro Fiven");
            return Json("");
        }
    }
}