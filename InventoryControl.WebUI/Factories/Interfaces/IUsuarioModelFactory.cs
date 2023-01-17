using AWASP.WebUI.ViewModels;

namespace AWASP.WebUI.Factories.Interfaces
{
    public interface IUsuarioModelFactory
    {
        Task<EditProfileViewModel> PrepareEditProfileModel(int userId);
        Task<RegisterViewModel> PrepareRegisterViewModel();
        Task<UsuarioViewModel> PrepareUsuarioModel(RegisterViewModel viewModel);
    }
}