using InventoryControl.Domain.Entities;
using InventoryControl.Models.Entities;

namespace InventoryControl.Application.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteModel> CreateCliente(ClienteModel model);
        Task<ClienteModel> UpdateCliente(ClienteModel model);
        Task<Cliente> FindByCpf(string cpf);
        Task<ClienteModel> FindById(int Id);
        Task<List<ClienteModel>> SearchClientes(ClienteModel model);
        Task UpdateCarga();
    }
}