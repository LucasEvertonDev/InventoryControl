using InventoryControl.WebUI.ViewModels.Servicos;

namespace InventoryControl.WebUI.Factories.Interfaces
{
    public interface IWhatsAppModelFactory
    {
        Task<WhatsAppViewModel> WhatsAppViewModel();
    }
}
