using InventoryControl.WebUI.ViewModels.Charts;
using InventoryControl.WebUI.ViewModels.Home;

namespace InventoryControl.WebUI.Factories.Interfaces
{
    public interface IHomeModelFactory
    {
        Task<Graph3SeriesViewModel> PrepareGraphViewModel();
        public Task<HomeViewModel> PrepareHomeViewModel();
    }
}