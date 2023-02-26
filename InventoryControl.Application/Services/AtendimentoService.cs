﻿using AutoMapper;
using InventoryControl.Application.Interfaces;
using InventoryControl.Application.Utils;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;
using InventoryControl.Models.Entities;
using InventoryControl.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InventoryControl.Application.Services
{
    public class AtendimentoService : Service, IAtendimentoService
    {
        private readonly IRepository<Atendimento> _atendimentoRepository;
        private readonly IRepository<Cliente> _clientRepository;
        private readonly IRepository<MapServicosAtendimento> _mapServicosAtendimentoRepository;
        private readonly IRepository<Servico> _servicoRepository;
        private readonly IRepository<Message> _messageRepository;
        private readonly IMapper _imapper;

        public AtendimentoService(IRepository<Atendimento> atendimentoRepository,
            IRepository<Cliente> repositoryClient,
            IRepository<MapServicosAtendimento> repositoryMapServicosAtendimento,
            IRepository<Servico> repositoryServico,
            IRepository<Message> messageRepository,
            IMapper mapper)
        {
            _atendimentoRepository = atendimentoRepository;
            _clientRepository = repositoryClient;
            _mapServicosAtendimentoRepository = repositoryMapServicosAtendimento;
            _servicoRepository = repositoryServico;
            _messageRepository = messageRepository;
            _imapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Atendimento> CreateAtendimento(AtendimentoModel model)
        {
            Atendimento atendimento = _imapper.Map<Atendimento>(model);
            var itens = await _atendimentoRepository.Table.Where(a => a.Data.Date == atendimento.Data.Date && a.Situacao != 1).ToListAsync();

            itens = itens.Where(a => Math.Abs((a.Data - atendimento.Data).TotalMinutes) < 60).ToList();

            if (itens != null && itens.Any())
            {
                LogicalException("Já existe um atendimento cadastrado para esse horário");
            }

            Cliente cliente = await _clientRepository.Table.Where(c => c.Id == atendimento.ClienteId).FirstOrDefaultAsync();

            atendimento.Cliente = cliente ?? LogicalException("Não foi possível recuperar o cliente");
            atendimento.IdExterno = Guid.NewGuid().ToString();
            atendimento.ClienteIdExterno = cliente.IdExterno;
            atendimento = await _atendimentoRepository.Insert(atendimento);

            foreach (var associacao in model.MapServicosAtendimen)
            {
                var servico = await _servicoRepository.Table.Where(s => s.Id == associacao.ServicoId).FirstOrDefaultAsync();
                var map = new MapServicosAtendimento
                {
                    Atendimento = atendimento,
                    ValorCobrado = associacao.ValorCobrado,
                    Servico = servico ?? LogicalException("Não foi possível recuperar o servico"),
                    IdExterno = Guid.NewGuid().ToString(),
                    AtendimentoIdExterno = atendimento.IdExterno,
                    ServicoIdExterno = servico.IdExterno,
                };

                await _mapServicosAtendimentoRepository.Insert(map);
            }

            await _atendimentoRepository.CommitAsync();

            var atendimentoFinal = await FindById(atendimento.Id);

            await _messageRepository.Insert(new Message
            {
                JsonMessage = JsonConvert.SerializeObject(atendimentoFinal),
                Situacao = (int)SituacaoMessage.AGUARDANDO_PROCESSAMENTO_MOBILE,
                TypeMessage = (int)TypeMessage.Atendimento
            });

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
            var atendimentoModel = _imapper.Map<AtendimentoModel>(atendimento);
            atendimentoModel.MapServicosAtendimen = _imapper.Map<List<AssociacaoServicosAtendimentoModel>>(atendimento.MapServicosAtendimentos);

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
            var atendimentos = await _atendimentoRepository.Table.Where(a => a.Data.Date >= dataInicio.Date && a.Data.Date <= dataFim.Date && a.Situacao != 1)?.Include(a => a.MapServicosAtendimentos).Include(c => c.Cliente).ToListAsync();
            var atendimentosModel = _imapper.Map<List<AtendimentoModel>>(atendimentos);
            atendimentosModel.ForEach(a =>
            {
                a.MapServicosAtendimen = _imapper.Map<List<AssociacaoServicosAtendimentoModel>>(atendimentos.Where(at => at.Id == a.Id).First().MapServicosAtendimentos);
            });
            return atendimentosModel.Distinct().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<AtendimentoModel>> SeachAgendamentos(AtendimentoModel model, DateTime dataInicio, DateTime datFim)
        {
            return _imapper.Map<List<AtendimentoModel>>(
                await _atendimentoRepository.Table
                    .Where(a => dataInicio <= a.Data.Date
                        && datFim >= a.Data.Date
                        && (model.ClienteId == -1 || model.ClienteId == a.ClienteId)
                        && (model.Situacao == -1 || model.Situacao == a.Situacao)
                    )
                    .Include(c => c.Cliente).ToListAsync()).OrderByDescending(a => a.Data).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<AtendimentoModel>> GetAtendimentos()
        {
            var list = (
                await _atendimentoRepository.Table
                    .Where(a => a.Situacao == 0)
                    .Include(c => c.MapServicosAtendimentos).ToListAsync()).OrderByDescending(a => a.Data).ToList();

            var atendimentos = new List<AtendimentoModel>();

            try 
            { 
                foreach(var atend in list)
                {
                    var atendimentoModel = _imapper.Map<AtendimentoModel>(atend);

                    atend.MapServicosAtendimentos.ToList().ForEach(servico =>
                    {
                        var servicoModel = new AssociacaoServicosAtendimentoModel()
                        { 
                            AtendimentoId = servico.AtendimentoId,
                            AtendimentoIdExterno = servico.AtendimentoIdExterno,
                            Id = servico.Id,
                            IdExterno = servico.IdExterno,
                            ServicoId = servico.ServicoId,
                            ServicoIdExterno = servico.ServicoIdExterno,
                            ValorCobrado = servico.ValorCobrado,
                        };

                        atendimentoModel.MapServicosAtendimen = new List<AssociacaoServicosAtendimentoModel>();
                        atendimentoModel.MapServicosAtendimen.Add(servicoModel);
                    });

                    atendimentos.Add(atendimentoModel);
                }
            }
            catch (Exception ex)
            {


            }


            return atendimentos;
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
            atendimento.ClienteIdExterno = cliente.IdExterno;
            atendimento = await _atendimentoRepository.Update(atendimento);

            var toUpdate = await _mapServicosAtendimentoRepository.Table.Where(a => a.AtendimentoId == model.Id).ToListAsync();
            foreach (var atend in toUpdate)
            {
                await _mapServicosAtendimentoRepository.Delete(atend);
            }

            foreach (var associacao in model.MapServicosAtendimen)
            {
                var servico = await _servicoRepository.Table.Where(s => s.Id == associacao.ServicoId).FirstOrDefaultAsync();
                var map = new MapServicosAtendimento
                {
                    Atendimento = atendimento,
                    ValorCobrado = associacao.ValorCobrado,
                    Servico = servico ?? LogicalException("Não foi possível recuperar o servico"),
                    IdExterno = Guid.NewGuid().ToString(),
                    AtendimentoIdExterno = atendimento.IdExterno,
                    ServicoIdExterno = servico.IdExterno,
                };

                await _mapServicosAtendimentoRepository.Insert(map);
            }

            await _atendimentoRepository.CommitAsync();

            var atendimentoFinal = await FindById(atendimento.Id);

            await _messageRepository.Insert(new Message
            {
                JsonMessage = JsonConvert.SerializeObject(atendimentoFinal),
                Situacao = (int)SituacaoMessage.AGUARDANDO_PROCESSAMENTO_MOBILE,
                TypeMessage = (int)TypeMessage.Atendimento
            });

            await _atendimentoRepository.CommitAsync();

            return atendimento;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task UpdateCarga()
        {
            try
            {
                var servicos = await _atendimentoRepository.Table.Include(a => a.MapServicosAtendimentos)
                    .Where(a => a.Situacao == 0).ToListAsync();
                  
                foreach (var servico in servicos)
                {
                    servico.IdExterno = Guid.NewGuid().ToString();
                    _atendimentoRepository.Update(servico);
                 
                    foreach (var i in servico.MapServicosAtendimentos)
                    {
                        i.IdExterno = Guid.NewGuid().ToString();
                        _mapServicosAtendimentoRepository.Update(i);
                    }
                }

                await _messageRepository.CommitAsync();
            }
            catch
            {
                throw;
            }
            finally
            {
                await _servicoRepository.CommitAsync();
            }
        }
    }
}
