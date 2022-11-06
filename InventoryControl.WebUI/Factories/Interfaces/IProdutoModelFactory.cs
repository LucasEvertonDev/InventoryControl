using InventoryControl.WebUI.ViewModels.Produto;

namespace InventoryControl.WebUI.Factories.Interfaces
{
    public interface IProdutoModelFactory
    {
        Task <ConsultaProdutoViewModel> ConsultaProdutoViewModel();
        Task<ProdutoViewModel> ProdutoViewModel();
        
    }
}
