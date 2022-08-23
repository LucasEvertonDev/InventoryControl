using InventoryControl.WebUI.Enuns;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryControl.WebUI.ViewModels.Atendimentos
{
    public class ConsultarAtendimentoViewModel
    {
        public DateTime? Data { get; set; }

        public string? ClienteId { get; set; }

        public List<AtendimentoViewModel> Atendimentos { get; set; }

        public SituacaoAtendimento SituacaoAtendimento { get; set; }

        public List<SelectListItem>? ComboClientes { get; set; }

        public bool Enabled { get; set; }

        public bool AutoComplete { get; set; }
    }
}
