using InventoryControl.WebUI.Identity.Entity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControl.WebUI.Controllers
{
    public class UsuarioController : BaseController 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            { 
                
            }
        }
    }
}
