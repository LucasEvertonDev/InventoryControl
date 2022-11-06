using InventoryControl.WebUI.Attributes;
using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.ViewModels.Produto;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControl.WebUI.Controllers
{
    public class ProdutoController : BaseController
    {
        private readonly IProdutoModelFactory _produtoModelFactory;
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoModelFactory produtoModelFactory)
        {
            _produtoModelFactory = produtoModelFactory;
        }

        [HttpGet, SessionExpire]
        public async Task<IActionResult> Index()
        {
            var viewModel = await _produtoModelFactory.ProdutoViewModel();
            return View(viewModel);
        }

        [HttpPost, SessionExpire]
        public async Task<IActionResult> Index(ProdutoViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpGet, SessionExpire]
        public async Task<IActionResult> Create()
        {
            var viewModel = await _produtoModelFactory.ProdutoViewModel();
            return View(viewModel);
        }
        [HttpPost, SessionExpire]
        public async Task<IActionResult> Create(ProdutoViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}
