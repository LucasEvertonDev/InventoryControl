using InventoryControl.Application.Utils;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControl.WebUI.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        { 
        }

        /// <summary>
        /// 
        /// </summary>
        public void TratarException(Exception ex)
        {
            if (ex.GetType() == typeof(LogicalException))
            {
                AddWarning(ex.Message);
            }
            else
            {
                AddError("Ocorreu um erro inesperado.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddError(string error)
        {
            TempData["success"] = null;
            TempData["warning"] = null;
            TempData["error"] = error;
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddSuccess(string sucess)
        {
            TempData["success"] = sucess;
            TempData["warning"] = null;
            TempData["error"] = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddWarning(string warning)
        {
            TempData["success"] = null;
            TempData["warning"] = warning;
            TempData["error"] = null;
        }
    }

}
