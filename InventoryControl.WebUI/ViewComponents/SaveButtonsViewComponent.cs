using InventoryControl.WebUI.Factories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControl.WebUI.ViewComponents
{
    [ViewComponent(Name = "SaveButtonsViewComponent")]
    public class SaveButtonsViewComponent : ViewComponent
    {
        private readonly ISaveButtonsModelFactory _SaveButtonsModelFactory;

        public SaveButtonsViewComponent(ISaveButtonsModelFactory SaveButtonsModelFactory)
        {
            this._SaveButtonsModelFactory = SaveButtonsModelFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("../Shared/_SaveButtons.cshtml", await _SaveButtonsModelFactory.PrepareSaveButtonsViewModel());
        }
    }
}
