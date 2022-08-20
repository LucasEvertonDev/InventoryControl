using InventoryControl.WebUI.ViewModels.Base;

namespace InventoryControl.WebUI.ViewModels.Components
{
    public class LayoutNavBarViewModel : BaseViewModel
    {
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public bool Enabled { get; set; }
    }
}
