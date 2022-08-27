namespace InventoryControl.Models.Entities
{
    public class CustosModel : BaseModel
    {
        public DateTime Data { get; set; }

        public decimal Valor { get; set; }

        public string Descricao { get; set; }
    }
}
