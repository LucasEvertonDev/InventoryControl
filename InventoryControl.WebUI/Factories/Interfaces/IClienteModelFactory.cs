using InventoryControl.WebUI.ViewModels.Clientes;

namespace InventoryControl.WebUI.Factories.Interfaces
{
    public interface IClienteModelFactory
    {
        Task<ClienteViewModel> PrepareClienteViewModel();
    }
}