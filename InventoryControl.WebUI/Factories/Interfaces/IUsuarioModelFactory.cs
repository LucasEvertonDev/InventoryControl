using InventoryControl.Application.Models;
using InventoryControl.WebUI.ViewModels;

namespace InventoryControl.WebUI.Factories.Interfaces
{
    public interface IUsuarioModelFactory
    {
        Task<EditProfileViewModel> PrepareEditProfileModel(int userId);
        Task<RegisterViewModel> PrepareRegisterViewModel();
        Task<UsuarioModel> PrepareUsuarioModel(RegisterViewModel viewModel);
    }
}
