using InventoryControl.WebUI.ViewModels.Components;

namespace InventoryControl.WebUI.Factories.Interfaces
{
    public interface ISaveButtonsModelFactory
    {
        Task<SaveButtonsViewModel> PrepareSaveButtonsViewModel();
    }
}