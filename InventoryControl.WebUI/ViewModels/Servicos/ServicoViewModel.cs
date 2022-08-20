using InventoryControl.Models.Entities;
using InventoryControl.WebUI.ViewModels.Base;

namespace InventoryControl.WebUI.ViewModels.Servicos
{
    public class ServicoViewModel : ServicoModel, IViewModel
    {
        public bool Enabled { get; set; } = true;

        public bool AutoComplete { get; set; }
    }
}
