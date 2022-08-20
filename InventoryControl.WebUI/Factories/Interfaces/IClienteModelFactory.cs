﻿using InventoryControl.Models.Entities;
using InventoryControl.WebUI.ViewModels.Clientes;

namespace InventoryControl.WebUI.Factories.Interfaces
{
    public interface IClienteModelFactory
    {
        Task<ClienteViewModel> PrepareClienteViewModel();

        Task<ClienteViewModel> PrepareClienteViewModel(ClienteModel usuario);

        Task<ClienteModel> PrepareClienteModelDto(ClienteViewModel clienteViewModel);

        Task<ConsultarClientesViewModel> PrepareConsultaClientesModel(List<ClienteModel> clientes);
    }
}