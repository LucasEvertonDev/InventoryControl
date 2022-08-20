using InventoryControl.Models.Entities;
using InventoryControl.WebUI.ViewModels.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryControl.WebUI.ViewModels.Atendimentos
{
    public class AtendimentoViewModel : AtendimentoModel, IViewModel
    {
        public List<SelectListItem>? ComboClientes { get; set; }

        public List<AssociacaoServicoAtendimentoViewModel> ServicosAssociados { get;set; }

        public int? ServicoId { get; set; }

        public bool Enabled { get; set; } = true;

        public bool AutoComplete { get; set; }

        public AtendimentoViewModel()
        { 
            ComboClientes = new List<SelectListItem>();
            ServicosAssociados = new List<AssociacaoServicoAtendimentoViewModel>();
        }
    }
}
