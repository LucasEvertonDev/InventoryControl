using AWASP.WebUI.Factories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AWASP.WebUI.ViewComponents
{
    [ViewComponent(Name = "NavigationPathViewComponent")]
    public class NavigationPathViewComponent : ViewComponent
    {
        private readonly INavigationPathFactory _navigationPathFactory;

        public NavigationPathViewComponent(INavigationPathFactory navigationPathFactory)
        {
            this._navigationPathFactory = navigationPathFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("../Shared/_NavigationPath.cshtml", await _navigationPathFactory.PrepareNavigationPathViewModel());
        }
    }
}
