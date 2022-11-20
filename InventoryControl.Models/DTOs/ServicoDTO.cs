namespace InventoryControl.Models.DTOs
{
    public class ServicoDTO : BaseDTO
    { 
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string? IdExterno { get; set; }
    }
}
