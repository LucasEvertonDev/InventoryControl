using InventoryControl.Application.Models;
using InventoryControl.WebUI.ViewModels;

namespace InventoryControl.WebUI.Factories.Interfaces
{
    public interface IUsuarioModelFactory
    {
        Task<RegisterViewModel> PrepareRegisterViewModel();
        Task<UsuarioModel> PrepareUsuarioModel(RegisterViewModel viewModel);
    }
}
