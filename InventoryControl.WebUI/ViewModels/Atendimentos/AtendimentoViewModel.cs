using InventoryControl.Models.Entities;
using InventoryControl.WebUI.Attributes;
using InventoryControl.WebUI.Enuns;
using InventoryControl.WebUI.ViewModels.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace InventoryControl.WebUI.ViewModels.Atendimentos
{
    public class AtendimentoViewModel : IViewModel
    {
        public int? Id { get; set; }

        [RequiredCustom]
        public DateTime Data { get; set; }

        [RequiredCustom]
        public int? ClienteId { get; set; }

        [RequiredCustom]
        public SimNao ClienteAtrasou { get; set; }

        public string? Cliente { get; set; }

        public string? ValorAtendimento { get; set; }

        public string? ValorPago { get; set; }

        public string? ObservacaoAtendimento { get; set; }

        public SituacaoAtendimento SituacaoAtendimento { get; set; }

        public bool Enabled { get; set; } = true;

        public bool AutoComplete { get; set; }

        public List<SelectListItem>? ComboClientes { get; set; }

        public List<AssociacaoServicoAtendimentoViewModel> ServicosAssociados { get; set; }
        public string? IdExterno { get; set; }
        public string? ClienteIdExterno { get; set; }

        public AtendimentoViewModel()
        { 
            ComboClientes = new List<SelectListItem>();
            ServicosAssociados = new List<AssociacaoServicoAtendimentoViewModel>();
        }
    }
}
