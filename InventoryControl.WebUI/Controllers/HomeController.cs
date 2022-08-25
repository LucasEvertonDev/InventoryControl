﻿using InventoryControl.WebUI.Attributes;
using InventoryControl.WebUI.Identity.Constants;
using InventoryControl.WebUI.ViewModels.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WhatsApp.SimpleCRM.Domain.Contracts.Service.Core;

namespace InventoryControl.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStartupService _startupService;

        public HomeController(ILogger<HomeController> logger,
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
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { });
            }
            return View();
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