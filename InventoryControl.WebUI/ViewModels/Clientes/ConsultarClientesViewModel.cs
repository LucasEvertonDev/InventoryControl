using InventoryControl.Models.Entities;
using InventoryControl.WebUI.ViewModels.Base;

namespace InventoryControl.WebUI.ViewModels.Clientes
{
    public class ConsultarClientesViewModel : IViewModel
    {
        public string? Cpf { get; set; }

        public string? Nome { get; set; }

        public List<ClienteModel> Clientes { get; set; }

        public bool Enabled { get; set; }

        public bool AutoComplete { get; set; }
    }
}
