﻿using AutoMapper;
using InventoryControl.Application.Interfaces;
using InventoryControl.Application.Utils;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;
using InventoryControl.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryControl.Application.Services
{
    public class AtendimentoService : Service, IAtendimentoService
    {
        private readonly IRepository<Atendimento> _atendimentoRepository;
        private readonly IRepository<Cliente> _clientRepository;
        private readonly IRepository<MapServicosAtendimento> _mapServicosAtendimentoRepository;
        private readonly IRepository<Servico> _servicoRepository;
        private readonly IMapper _imapper;

        public AtendimentoService(IRepository<Atendimento> atendimentoRepository,
            IRepository<Cliente> repositoryClient,
            IRepository<MapServicosAtendimento> repositoryMapServicosAtendimento,
            IRepository<Servico> repositoryServico,
            IMapper mapper)
        {
            _atendimentoRepository = atendimentoRepository;
            _clientRepository = repositoryClient;
            _mapServicosAtendimentoRepository = repositoryMapServicosAtendimento;
            _servicoRepository = repositoryServico;
            _imapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Atendimento> CreateAtendimento(AtendimentoModel model)
        {
            Atendimento atendimento = _imapper.Map<Atendimento>(model);
            var itens = await _atendimentoRepository.Table.Where(a => a.Data.Date == atendimento.Data.Date).ToListAsync();

            itens = itens.Where(a => Math.Abs((a.Data - atendimento.Data).TotalMinutes) < 60).ToList();

            if (itens != null && itens.Any())
            {
                LogicalException("Já existe um atendimento cadastrado para esse horário");
            }

            Cliente cliente = await _clientRepository.Table.Where(c => c.Id == atendimento.ClienteId).FirstOrDefaultAsync();

            atendimento.Cliente = cliente ?? LogicalException("Não foi possível recuperar o cliente");
            atendimento = await _atendimentoRepository.Insert(atendimento);

            foreach (var associacao in model.ServicosAssociados)
            {
                var servico = await _servicoRepository.Table.Where(s => s.Id == associacao.ServicoId).FirstOrDefaultAsync();
                var map = new MapServicosAtendimento
                {
                    Atendimento = atendimento,
                    ValorCobrado = associacao.ValorCobrado,
                    Servico = servico ?? LogicalException("Não foi possível recuperar o servico"),
                };

                await _mapServicosAtendimentoRepository.Insert(map);
            }

            await _atendimentoRepository.CommitAsync();

            return atendimento;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AtendimentoModel> FindById(int id)
        {
            var atendimento = await _atendimentoRepository.Table.Where(a => a.Id == id)?.Include(a => a.MapServicosAtendimentos).FirstOrDefaultAsync();
            var atendimentoModel = _imapper.Map<AtendimentoModel>(await _atendimentoRepository.Table.Where(a => a.Id == id)?.Include(a => a.MapServicosAtendimentos).FirstOrDefaultAsync());
            atendimentoModel.ServicosAssociados = _imapper.Map<List<AssociacaoServicosAtendimentoModel>>(atendimento.MapServicosAtendimentos);

            return atendimentoModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataInicio"></param>
        /// <param name="dataFim"></param>
        /// <returns></returns>
        public async Task<List<AtendimentoModel>> SeachAgendamentos(DateTime dataInicio, DateTime dataFim)
        {
            return _imapper.Map<List<AtendimentoModel>>(await _atendimentoRepository.Table.Where(a => a.Data.Date >= dataInicio.Date && a.Data.Date <= dataFim.Date).Include(c => c.Cliente).ToListAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<AtendimentoModel>> SeachAgendamentos(AtendimentoModel model)
        {
            return _imapper.Map<List<AtendimentoModel>>(
                await _atendimentoRepository.Table
                    //.Where(a => !model.Data.HasValue && a.Data.Date == model.Data.Value.Date
                    //    && model.ClienteId == -1 || model.ClienteId == a.ClienteId
                    //    && model.Situacao == -1 || model.Situacao == a.Situacao
                    //)
                    .Include(c => c.Cliente).ToListAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Atendimento> UpdateAtendimento(AtendimentoModel model)
        {
            Atendimento atendimento = _imapper.Map<Atendimento>(model);

            Cliente cliente = await _clientRepository.Table.Where(c => c.Id == atendimento.ClienteId).FirstOrDefaultAsync();

            atendimento.Cliente = cliente ?? LogicalException("Não foi possível recuperar o cliente");
            atendimento = await _atendimentoRepository.Update(atendimento);

            foreach (var associacao in model.ServicosAssociados)
            {
                var servico = await _servicoRepository.Table.Where(s => s.Id == associacao.ServicoId).FirstOrDefaultAsync();
                var map = new MapServicosAtendimento
                {
                    Id = associacao.Id.Value,
                    Atendimento = atendimento,
                    ValorCobrado = associacao.ValorCobrado,
                    Servico = servico ?? LogicalException("Não foi possível recuperar o servico"),
                };

                await _mapServicosAtendimentoRepository.Update(map);
            }

            await _atendimentoRepository.CommitAsync();

            return atendimento;
        }
    }
}
