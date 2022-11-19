using InventoryControl.Domain.Entities;
using InventoryControl.Models.Entities;

namespace InventoryControl.Application.Interfaces
{
    public interface IServicosService
    {
        Task<ServicoModel> CreateServico(ServicoModel model);
        Task<ServicoModel> FindById(int Id);
        Task<Servico> FindByName(string name);
        Task<List<ServicoModel>> SearchServicos(ServicoModel model);
        Task UpdateCarga();
        Task<ServicoModel> UpdateServico(ServicoModel model);
    }
}