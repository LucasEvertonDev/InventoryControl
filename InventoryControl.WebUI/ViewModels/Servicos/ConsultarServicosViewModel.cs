using InventoryControl.Models.Entities;
using InventoryControl.WebUI.ViewModels.Base;

namespace InventoryControl.WebUI.ViewModels.Servicos
{
    public class ConsultarServicosViewModel : IViewModel
    {
        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public List<ServicoModel> Servicos { get; set; }

        public bool Enabled { get; set; }

        public bool AutoComplete { get; set; }
    }
}