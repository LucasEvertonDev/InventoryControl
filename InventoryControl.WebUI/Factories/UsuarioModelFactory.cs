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
                Login = viewModel.Login,
                Nome = viewModel.Email,
                PerfilUsuarioId = viewModel.PerfilUsuarioId,
                Senha = viewModel.Senha,
                Situacao = 1
            });
        }
    }
}
