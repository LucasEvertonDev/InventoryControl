using InventoryControl.Application.Interfaces;
using InventoryControl.WebUI.Extensions;
using InventoryControl.WebUI.Identity;
using InventoryControl.WebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControl.WebUI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public IUsuarioService _usuarioService { get; }

        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager,
           UserManager<ApplicationUser> userManager,
           IUsuarioService usuarioService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            AddSuccess("Sucesso é isso");
            var loginViewModel = new LoginViewModel();
            return View(loginViewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    var user = await _usuarioService.Login(loginViewModel.Username, loginViewModel.Password);
                    if (user != null && user.Id > 0)
                    {
                        var result = await _signInManager.PasswordSignInAsync(
                           loginViewModel.Username,
                           loginViewModel.Password,
                           loginViewModel.RememberMe,
                           lockoutOnFailure: false);

                        HttpContext.Session.Set("UsuarioId", user.Id);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TratarException(ex);
            }
            return View(loginViewModel);
        }
    }
}
