using InventoryControl.WebUI.ViewModels.Components;

namespace InventoryControl.WebUI.Factories.Interfaces
{
    public interface INavigationPathFactory
    {
        Task<NavigationPathViewModel> PrepareNavigationPathViewModel();
    }
}