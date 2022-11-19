namespace InventoryControl.Models.Entities
{
    public class ServicoModel : BaseModel
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string? IdExterno { get; set; }
    }
}
