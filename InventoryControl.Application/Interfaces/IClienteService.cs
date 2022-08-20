using InventoryControl.Domain.Entities;
using InventoryControl.Models.Entities;

namespace InventoryControl.Application.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteModel> CreateCliente(ClienteModel model);
        Task<Cliente> FindByCpf(string cpf);
        Task<List<ClienteModel>> SearchClientes(ClienteModel model);
    }
}