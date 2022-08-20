using InventoryControl.Models.Entities;
using InventoryControl.WebUI.ViewModels.Account;

namespace InventoryControl.WebUI.Factories.Interfaces
{
    public interface IUsuarioModelFactory
    {
        Task<EditProfileViewModel> PrepareEditProfileModel(int userId);
        Task<RegisterViewModel> PrepareRegisterViewModel();
        Task<UsuarioModel> PrepareUsuarioModel(RegisterViewModel viewModel);
    }
}
