using InventoryControl.Application.Interfaces;
using InventoryControl.Models.Entities;
using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.ViewModels.Clientes;

namespace InventoryControl.WebUI.Factories
{
    public class ClienteModelFactory : IClienteModelFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<ClienteViewModel> PrepareClienteViewModel()
        {
            return Task.FromResult(new ClienteViewModel { DataNascimento = null });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ClienteViewModel> PrepareClienteViewModel(ClienteModel clienteModel)
        {
            return Task.FromResult(new ClienteViewModel 
            {
                Cpf = clienteModel.Cpf,
                DataNascimento = clienteModel.DataNascimento,
                Id = clienteModel.Id,
                Nome = clienteModel.Nome,
                Telefone = clienteModel.Telefone
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<ClienteModel> PrepareClienteModelDto(ClienteViewModel clienteViewModel)
        {
            return Task.FromResult(new ClienteModel 
            {
                Cpf = String.IsNullOrEmpty(clienteViewModel.Cpf) ? "" :  String.Join("", System.Text.RegularExpressions.Regex.Split(clienteViewModel.Cpf, @"[^\d]")),
                DataNascimento = clienteViewModel.DataNascimento,
                Id = clienteViewModel.Id,
                Nome = clienteViewModel.Nome,
                Telefone = String.IsNullOrEmpty(clienteViewModel.Telefone) ? "" : String.Join("", System.Text.RegularExpressions.Regex.Split(clienteViewModel.Telefone, @"[^\d]")),
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientes"></param>
        /// <returns></returns>
        public Task<ConsultarClientesViewModel> PrepareConsultaClientesModel(List<ClienteModel> clientes)
        {
            return Task.FromResult(new ConsultarClientesViewModel
            {
                Clientes = clientes
            });
        }
    }
}
