namespace InventoryControl.Models.DTOs
{
    public class MessageDTO  : BaseDTO
    {
        public string? JsonMessage { get; set; }
        public int? TypeMessage { get; set; }
        public int? Situacao { get; set; }
    }
}
