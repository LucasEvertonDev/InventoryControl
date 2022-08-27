using InventoryControl.Models.Entities;

namespace InventoryControl.Application.Interfaces
{
    public interface ICustosService
    {
        Task<CustosModel> CreateCusto(CustosModel model);
        Task<CustosModel> FindById(int Id);
        Task<List<CustosModel>> SearchCustos(CustosModel model, DateTime dataInicio, DateTime dataFim);
        Task<CustosModel> UpdateCusto(CustosModel model);
    }
}