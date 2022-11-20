using System.ComponentModel.DataAnnotations;

namespace InventoryControl.Models.Entities
{
    public class ClienteModel : BaseModel
    {
        public string? Cpf { get; set; }
        [Required]
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Telefone { get; set; }
        public string? IdExterno { get; set; }
    }
}
