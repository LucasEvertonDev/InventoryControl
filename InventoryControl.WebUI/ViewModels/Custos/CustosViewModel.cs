using InventoryControl.WebUI.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace InventoryControl.WebUI.ViewModels.Custos
{
    public class CustosViewModel : BaseViewModel, IViewModel
    {
        [Required]
        public DateTime Data { get; set; }

        [Required]
        public string Valor { get; set; }

        [Required]
        public string Descricao { get; set; }

        public bool Enabled { get; set; } = true;

        public bool AutoComplete { get; set; }
    }
}
