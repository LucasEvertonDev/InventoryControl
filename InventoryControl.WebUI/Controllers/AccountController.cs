using InventoryControl.Application.Interfaces;
using InventoryControl.WebUI.Attributes;
using InventoryControl.WebUI.Extensions;
using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.Identity;
using InventoryControl.WebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InventoryControl.WebUI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUsuarioService _usuarioService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUsuarioModelFactory _usuarioModelFactory;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager,
           UserManager<ApplicationUser> userManager,
           IUsuarioService usuarioService,
           IHttpContextAccessor httpContextAccessor,
           IUsuarioModelFactory usuarioModelFactory)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _usuarioService = usuarioService;
            _httpContextAccessor = httpContextAccessor;
            _usuarioModelFactory = usuarioModelFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            var remember = GetCookie("RememberMe");
            var loginViewModel = new LoginViewModel()
            {
                RememberMe = "True".Equals(remember),
                ReturnUrl = returnUrl
            };
            return View(loginViewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
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

                        HttpContext.Session.Set("UserId", user.Id);
                        UpdateCookie("RememberMe", loginViewModel.RememberMe.ToString());

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
            loginViewModel.RememberMe = false;
            return View(loginViewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            var viewModel = await _usuarioModelFactory.PrepareRegisterViewModel();
            return View(viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            try
            {
                viewModel.Perfis = (await _usuarioModelFactory.PrepareRegisterViewModel()).Perfis;

                if (viewModel.Password != viewModel.ConfirmPassword)
                {
                    ModelState.AddModelError(nameof(viewModel.ConfirmPassword), "Passwords do not match.");
                }

                if(ModelState.IsValid)
                {
                    var usuario = await _usuarioService.CreateUsuario(
                        await _usuarioModelFactory.PrepareUsuarioModel(viewModel));

                    if (usuario.Id > 0)
                    {
                        AddSuccess("User registered successfully");
                        return RedirectToAction("Login", "Account");
                    }
                }
            }
            catch (Exception e)
            {
                TratarException(e);
            }
            return View(viewModel);
        }

        [HttpGet, Authorize, SessionExpire]
        public async Task<IActionResult> EditProfile()
        {
            var viewModel = await _usuarioModelFactory.PrepareEditProfileModel(HttpContext.Session.Get<int>("UserId"));
            return View(viewModel);
        }

        [HttpPost, Authorize, SessionExpire]
        public IActionResult EditProfile(EditProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            { 
            }
            return View(viewModel);
        }
    }   
}
