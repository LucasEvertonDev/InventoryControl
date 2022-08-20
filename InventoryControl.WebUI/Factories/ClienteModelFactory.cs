using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.ViewModels.Clientes;

namespace InventoryControl.WebUI.Factories
{
    public class ClienteModelFactory : IClienteModelFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<ClienteViewModel> PrepareClienteViewModel()
        {
            return Task.FromResult(new ClienteViewModel { DataNascimento = DateTime.Now.Date });
        }
    }
}
