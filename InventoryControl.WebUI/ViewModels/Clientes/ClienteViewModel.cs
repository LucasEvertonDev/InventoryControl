using InventoryControl.Models.Entities;
using InventoryControl.WebUI.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace InventoryControl.WebUI.ViewModels.Clientes
{
    public class ClienteViewModel : ClienteModel, IViewModel
    {
        public bool Enabled { get; set; } = true;

        public bool AutoComplete { get; set; }
    }
}
