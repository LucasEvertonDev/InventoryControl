using InventoryControl.Models.Entities;
using InventoryControl.WebUI.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace InventoryControl.WebUI.ViewModels.Atendimentos
{
    public class AssociacaoServicoAtendimentoViewModel 
    {
        public int? Id { get; set; }

        public int? AtendimentoId { get; set; }

        [RequiredCustom]
        public int ServicoId { get; set; }

        [RequiredCustom]
        public string ValorCobrado { get; set; }

        public string NomeLista { get; set; } = "ServicosAssociados";

        public int PosicaoLista { get; set; }

        public bool Apagado { get; set; }

        public List<SelectListItem>? ComboServicos { get; set; }

        public string GetPropName(string propName) => string.Concat(NomeLista, $"[{PosicaoLista}]", string.IsNullOrEmpty(propName) ? "" : $".{propName}");
    }
}
