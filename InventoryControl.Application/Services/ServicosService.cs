using AutoMapper;
using InventoryControl.Application.Interfaces;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;
using InventoryControl.Models.Entities;
using InventoryControl.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InventoryControl.Application.Services
{
    public class ServicosService : Service, IServicosService
    {
        private readonly IRepository<Servico> _servicoRepository;
        public readonly IMapper _imapper;
        private readonly IRepository<Message> _messageRepository;
        private readonly IMessageService _messageService;

        public ServicosService(
            IRepository<Servico> servicoRepository,
            IRepository<Message> messageRepository,
            IMessageService messageService,
            IMapper imapper)
        {
            _servicoRepository = servicoRepository;
            _messageRepository = messageRepository;
            this._messageService = messageService;
            _imapper = imapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Servico> FindByName(string name)
        {
            return await _servicoRepository.Table.Where(a => a.Nome.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ServicoModel> FindById(int Id)
        {
            return _imapper.Map<ServicoModel>(await _servicoRepository.FindById(Id));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ServicoModel> CreateServico(ServicoModel model)
        {
            var servico = _imapper.Map<Servico>(model);
            if (await this.FindByName(model.Nome) != null)
            {
                LogicalException("Já existe um servico cadastrado com esse nome");
            }
            servico.IdExterno = Guid.NewGuid().ToString();
            servico = await _servicoRepository.Insert(servico);

            _messageRepository.Insert(new Message
            {
                JsonMessage = JsonConvert.SerializeObject(servico),
                Situacao = (int)SituacaoMessage.AGUARDANDO_PROCESSAMENTO_MOBILE,
                TypeMessage = (int)TypeMessage.Servico
            });

            await _servicoRepository.CommitAsync();

            return _imapper.Map<ServicoModel>(servico);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ServicoModel>> SearchServicos(ServicoModel model)
        {
            var servicos = await _servicoRepository.Table.Where(a => (string.IsNullOrEmpty(model.Nome) || a.Nome == model.Nome)
                    && (string.IsNullOrEmpty(model.Descricao) || a.Descricao.ToLower().Contains(model.Descricao.ToLower()))).ToListAsync();

            return _imapper.Map<List<ServicoModel>>(servicos).OrderBy(a => a.Nome).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ServicoModel> UpdateServico(ServicoModel model)
        {
            var servico = _imapper.Map<Servico>(model);

            servico = await _servicoRepository.Update(servico);
            await _servicoRepository.CommitAsync();

            return _imapper.Map<ServicoModel>(servico);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task UpdateCarga()
        {
            try
            {
                var servicos = await _servicoRepository.Table.ToListAsync();
                foreach (var servico in servicos)
                {
                    servico.IdExterno = Guid.NewGuid().ToString();
                    _servicoRepository.Update(servico);
                    _messageRepository.Insert(new Message
                    {
                        JsonMessage = JsonConvert.SerializeObject(servico),
                        Situacao = (int)SituacaoMessage.AGUARDANDO_PROCESSAMENTO_MOBILE,
                        TypeMessage = (int)TypeMessage.Servico
                    });
                }
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
