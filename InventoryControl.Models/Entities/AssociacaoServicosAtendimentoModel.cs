using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace InventoryControl.Models.Entities
{
    public class AssociacaoServicosAtendimentoModel : BaseModel
    {
        public int ServicoId { get; set; }
        public int AtendimentoId { get; set; }
        public decimal? ValorCobrado { get; set; }
        public string? IdExterno { get; set; }
        public string? ServicoIdExterno { get; set; }
        public string? AtendimentoIdExterno { get; set; }
        [JsonIgnore]
        public ServicoModel Servico { get; set; }
        [JsonIgnore]
        public AtendimentoModel Atendimento { get; set; }
    }
}
