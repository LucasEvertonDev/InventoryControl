using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.ViewModels.Servicos;

namespace InventoryControl.WebUI.Factories
{
    public class WhatsAppModelFactory : IWhatsAppModelFactory
    {
        public Task<WhatsAppViewModel> WhatsAppViewModel()
        {
            return Task.FromResult(new WhatsAppViewModel {});
        }
    }
}
