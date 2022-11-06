using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.ViewModels.Produto;

namespace InventoryControl.WebUI.Factories
{
    public class ProdutoModelFactory : IProdutoModelFactory
    {
        public Task<ProdutoViewModel> ProdutoViewModel()
        {
            return Task.FromResult(new ProdutoViewModel { });
        }
        public Task<ConsultaProdutoViewModel> ConsultaProdutoViewModel()
        { 
            return Task.FromResult(new ConsultaProdutoViewModel { });
        }
    }
}
   