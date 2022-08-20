using InventoryControl.WebUI.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace InventoryControl.WebUI.ViewModels.Clientes
{
    public class ClienteViewModel : BaseViewModel, IViewModel
    {
        [Required]
        public string Cpf { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        [Required]
        public string Telefone { get; set; }

        public bool Enabled { get; set; }
    }
}
