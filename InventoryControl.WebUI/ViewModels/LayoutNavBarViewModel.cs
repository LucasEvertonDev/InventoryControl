namespace InventoryControl.WebUI.ViewModels
{
    public class LayoutNavBarViewModel : BaseViewModel, IViewModel
    {
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public bool Enabled { get; set; }
    }
}
