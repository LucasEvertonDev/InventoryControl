using InventoryControl.Models.DTOs;
using Newtonsoft.Json;

namespace InventoryControl.Models.Entities
{
    public class AtendimentoDTO : BaseDTO
    {
        public string Data { get; set; }

        public int ClienteId { get; set; }
        [JsonIgnore]
        public ClienteModel Cliente { get; set; }

        public bool ClienteAtrasado { get; set; }

        public decimal ValorAtendimento { get; set; }

        public decimal? ValorPago { get; set; }

        public string? ObservacaoAtendimento { get; set; }

        public int Situacao { get; set; }
        public string? IdExterno { get; set; }
        public string? ClienteIdExterno { get; set; }

        public List<AssociacaoServicosAtendimentoModel> ServicosAssociados { get; set; }
    }
}
