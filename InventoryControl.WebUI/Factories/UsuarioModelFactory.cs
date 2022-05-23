using InventoryControl.Application.Interfaces;
using InventoryControl.Application.Models;
using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryControl.WebUI.Factories
{
    public class UsuarioModelFactory : IUsuarioModelFactory
    {
        public IUsuarioService _usuarioService { get; }

        public UsuarioModelFactory(
            IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<RegisterViewModel> PrepareRegisterViewModel()
        {
            var perfis = await _usuarioService.FindPerfisUsuario();
            return new RegisterViewModel()
            {
                Perfis = perfis.Select(p => new SelectListItem
                {
                    Text = p.Nome,
                    Value = p.Id.ToString(),
                }).ToList(),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<UsuarioModel> PrepareUsuarioModel(RegisterViewModel viewModel)
        {
            return Task.FromResult(new UsuarioModel
            { 
                Email = viewModel.Email,
                Login = viewModel.Username,
                Nome = viewModel.Email,
                PerfilUsuarioId = viewModel.PerfilUsuarioId,
                Senha = viewModel.Password,
                Situacao = 1
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<EditProfileViewModel> PrepareEditProfileModel(int userId)
        { 
            var user = await _usuarioService.FindById(userId);
            var perfis = await _usuarioService.FindPerfisUsuario();
          
            var viewModel = new EditProfileViewModel()
            { 
                Email = user.Email,
                Username = user.Login,
                PerfilUsuarioId = user.PerfilUsuarioId,
                Perfis = perfis.Select(p => new SelectListItem
                {
                    Text = p.Nome,
                    Value = p.Id.ToString(),
                }).ToList(),
            };
            return viewModel;
        }
    }
}
