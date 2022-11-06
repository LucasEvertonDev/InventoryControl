using System.ComponentModel.DataAnnotations;

namespace InventoryControl.Models.Entities
{
    public class ProdutoModel : BaseModel
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public string Categoria { get; set; }
        [Required]
        public string preco { get; set; }
    }
}
