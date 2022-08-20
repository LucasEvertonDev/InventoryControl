using InventoryControl.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryControl.WebUI.ViewModels.Atendimentos
{
    public class AssociacaoServicoAtendimentoViewModel : AssociacaoServicosAtendimentoModel
    {
        public int PosicaoLista { get; set; }

        public bool Apagado { get; set; }

        public List<SelectListItem>? ComboServicos { get; set; }
    }
}
