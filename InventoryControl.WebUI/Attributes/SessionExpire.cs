using AWASP.WebUI.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AWASP.WebUI.Attributes
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
            if (string.IsNullOrEmpty(session.Get<int>("UserId").ToString()) || session.Get<int>("UserId") == 0)
            {
                filterContext.Result = new RedirectToActionResult("Login", "Account", new { });
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
