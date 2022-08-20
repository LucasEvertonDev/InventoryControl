namespace InventoryControl.WebUI.ViewModels.Base
{
    public interface IViewModel
    {
        bool Enabled { get; set; }

        bool AutoComplete { get; set; }
    }
}
