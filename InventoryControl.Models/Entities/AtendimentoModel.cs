namespace InventoryControl.Models.Entities
{
    public class AtendimentoModel : BaseModel
    {
        public DateTime Data { get; set; }

        public int ClienteId { get; set; }

        public ClienteModel Cliente { get; set; }

        public bool ClienteAtrasado { get; set; }

        public decimal ValorAtendimento { get; set; }

        public decimal? ValorPago { get; set; }

        public string? ObservacaoAtendimento { get; set; }

        public int Situacao { get; set; }

        public List<AssociacaoServicosAtendimentoModel> ServicosAssociados { get; set; }
    }
}
