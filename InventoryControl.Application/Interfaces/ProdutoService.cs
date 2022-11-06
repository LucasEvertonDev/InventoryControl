using InventoryControl.Models.Entities;

namespace InventoryControl.Application.Interfaces
{
    public interface ProdutoService
    {
        Task<ProdutoModel> CreateProduto(ProdutoModel model);
    }
}
