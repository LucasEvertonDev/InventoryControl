using InventoryControl.Models.Entities;
using InventoryControl.WebUI.ViewModels.Base;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace InventoryControl.WebUI.ViewModels.Produto
{
    public class ProdutoViewModel : ProdutoModel, IViewModel
    {
        public bool Enabled { get; set; } = true;
        public bool AutoComplete { get ; set ; }
    }
}
