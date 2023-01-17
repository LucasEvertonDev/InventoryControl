using AWASP.WebUI.ViewModels;

namespace AWASP.WebUI.Factories.Interfaces
{
    public interface ISaveButtonsModelFactory
    {
        Task<SaveButtonsViewModel> PrepareSaveButtonsViewModel();
    }
}