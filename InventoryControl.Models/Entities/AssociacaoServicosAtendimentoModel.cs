using System.ComponentModel.DataAnnotations;

namespace InventoryControl.Models.Entities
{
    public class AssociacaoServicosAtendimentoModel : BaseModel
    {
        public int ServicoId { get; set; }
        public int AtendimentoId { get; set; }
        public decimal? ValorCobrado { get; set; }

        public ServicoModel Servico { get; set; }
        public AtendimentoModel Atendimento { get; set; }
    }
}
