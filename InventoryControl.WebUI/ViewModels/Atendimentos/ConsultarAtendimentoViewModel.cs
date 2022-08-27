using InventoryControl.WebUI.Enuns;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryControl.WebUI.ViewModels.Atendimentos
{
    public class ConsultarAtendimentoViewModel
    {
        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public string? ClienteId { get; set; }

        public List<AtendimentoViewModel> Atendimentos { get; set; }

        public SituacaoAtendimentoconsulta SituacaoAtendimento { get; set; }

        public List<SelectListItem>? ComboClientes { get; set; }

        public bool Enabled { get; set; }

        public bool AutoComplete { get; set; }
    }
}
