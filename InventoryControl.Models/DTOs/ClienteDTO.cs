using InventoryControl.Models.DTOs;

namespace InventoryControl.Models.Dto
{
    public class ClienteDTO: BaseDTO
    {
        public string? Cpf { get; set; }
        public string Nome { get; set; }
        public string? DataNascimento { get; set; }
        public string? Telefone { get; set; }
        public string? IdExterno { get; set; }
    }
}
