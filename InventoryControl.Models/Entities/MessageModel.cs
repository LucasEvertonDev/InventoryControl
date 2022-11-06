namespace InventoryControl.Models.Entities
{
    public class MessageModel : BaseModel
    {
        public string? JsonMessage { get; set; }
        public int? TypeMessage { get; set; }
        public int? Situacao { get; set; }
    }
}
