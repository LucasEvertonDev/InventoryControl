using InventoryControl.WebUI.Identity;
using InventoryControl.WebUI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControl.WebUI
{
    [ViewComponent(Name = "LayoutNaveBarViewComponent")]
    public class LayoutNaveBarViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public LayoutNaveBarViewComponent(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var appUser = await _userManager.FindByNameAsync(
                HttpContext.User.Identity.Name);

            var vm = new LayoutNavBarViewModel
            {
                UserName = appUser?.UserName,
                Email = appUser?.Email,
            };

            return View("../Shared/_LayoutNavBar.cshtml", vm);
        }
    }
}
