using InventoryControl.WebUI.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InventoryControl.WebUI.Attributes
{
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ISession session = filterContext.HttpContext.Session;
            // check  sessions here
            if (session.Get<string>("IdUsuario") == null)
            {
                filterContext.Result = new RedirectToActionResult("Login", "Usuario", new {});
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
