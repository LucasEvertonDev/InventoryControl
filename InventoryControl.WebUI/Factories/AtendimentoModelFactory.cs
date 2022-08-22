﻿using InventoryControl.Application.Interfaces;
using InventoryControl.Models.Entities;
using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.ViewModels.Atendimentos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryControl.WebUI.Factories
{
    public class AtendimentoModelFactory : IAtendimentoModelFactory
    {
        private IServicosService _servicosService;
        private readonly IClienteService _clienteService;

        public AtendimentoModelFactory(IServicosService servicosService,
            IClienteService clienteService)
        {
            _servicosService = servicosService;
            _clienteService = clienteService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<AtendimentoViewModel> PrepareAtendimentoViewModel()
        {
            var servicos = await _servicosService.SearchServicos(new Models.Entities.ServicoModel() { });
            var clientes = await _clienteService.SearchClientes(new Models.Entities.ClienteModel() { });

            return new AtendimentoViewModel
            {
                Data = DateTime.Now.Date,
                ComboClientes = clientes.Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString() }).ToList(),
                ServicosAssociados = new List<AssociacaoServicoAtendimentoViewModel>()
                {
                    new ViewModels.Atendimentos.AssociacaoServicoAtendimentoViewModel
                    {
                        PosicaoLista = 0,
                        Apagado = false,
                        ComboServicos = servicos.Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString() }).ToList(),
                    }
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<AtendimentoViewModel> PrepareAtendimentoViewModel(AtendimentoViewModel atendimentoViewModel)
        {
            var servicos = await _servicosService.SearchServicos(new Models.Entities.ServicoModel() { });
            var clientes = await _clienteService.SearchClientes(new Models.Entities.ClienteModel() { });

            atendimentoViewModel.ComboClientes = clientes.Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString() }).ToList();
            atendimentoViewModel.ServicosAssociados.ForEach(a => a.ComboServicos = servicos.Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString() }).ToList());

            return atendimentoViewModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public Task<AtendimentoModel> PrepareAtendimentoModelDto(AtendimentoViewModel viewModel)
        {
            return Task.FromResult(new AtendimentoModel
            {
                ClienteAtrasado = viewModel.ClienteAtrasou == Enuns.SimNao.SIM,
                ClienteId = viewModel.ClienteId.GetValueOrDefault(),
                Data = viewModel.Data,
                Id = viewModel.Id,
                ObservacaoAtendimento = viewModel.ObservacaoAtendimento,
                Situacao = (int)viewModel.SituacaoAtendimento,
                ValorAtendimento = string.IsNullOrEmpty(viewModel.ValorAtendimento) ? 0 : decimal.Parse(viewModel.ValorAtendimento),
                ValorPago = string.IsNullOrEmpty(viewModel.ValorPago) ? null : decimal.Parse(viewModel.ValorPago),
                ServicosAssociados = viewModel.ServicosAssociados.Where(a => !a.Apagado).Select(a => 
                    new AssociacaoServicosAtendimentoModel
                    {
                        AtendimentoId = viewModel.Id.GetValueOrDefault(),
                        ServicoId = a.ServicoId,
                        ValorCobrado = string.IsNullOrEmpty(a.ValorCobrado) ? null : decimal.Parse(a.ValorCobrado),
                        Id = a.Id,
                    }).ToList()
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public Task<List<CalendarioViewModel>> PrepareCalendaViewModel(List<AtendimentoModel> listModel)
        {
            var atendimentos = new List<CalendarioViewModel>();
            foreach (var a in listModel)
            {
                atendimentos.Add(new CalendarioViewModel
                {
                    id = a.Id.ToString(),
                    title = " - " + a.Cliente.Nome.Split(" ")[0],
                    color = a.Data < DateTime.Now.Date ? "#FF7F50" : "#66CDAA",
                    descricao = "Atendimento para a cliente j",
                    tooltip = "Ás " + a.Data.Hour.ToString() + " horas",
                    start = a.Data.ToString("yyyy-MM-ddTHH:mm"),
                });
            }
            return Task.FromResult(atendimentos);
        }
    }
}
