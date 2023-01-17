using AWASP.WebUI.ViewModels;

namespace AWASP.WebUI.Factories.Interfaces
{
    public interface INavigationPathFactory
    {
        Task<NavigationPathViewModel> PrepareNavigationPathViewModel();
    }
}