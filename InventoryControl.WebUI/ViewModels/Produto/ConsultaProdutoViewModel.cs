using InventoryControl.WebUI.ViewModels.Base;

namespace InventoryControl.WebUI.ViewModels.Produto
{
    public class ConsultaProdutoViewModel : IViewModel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string preco { get; set; }
        public bool Enabled { get ; set ; } = true;
        public bool AutoComplete { get ; set ; }
    }
}
