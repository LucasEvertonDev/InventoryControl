using InventoryControl.WebUI.ViewModels.Home;

namespace InventoryControl.WebUI.Factories.Interfaces
{
    public interface IHomeModelFactory
    {
        public Task<HomeViewModel> PrepareHomeViewModel();
    }
}