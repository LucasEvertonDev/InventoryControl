using InventoryControl.Api.Contracts;
using InventoryControl.Api.Factorys.Interfaces;
using InventoryControl.Application.Interfaces;
using InventoryControl.Models.DTOs;
using InventoryControl.Models.Entities;
using InventoryControl.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InventoryControl.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;
        private readonly IMessageService _messageService;
        private readonly IClienteService _clienteService;
        private readonly IServicosService _servicosService;
        private readonly IMessageModelFactory _messageModelFactory;
        private readonly IClienteModelFactory _clienteModelFactory;
        private readonly IServicoModelFactory _servicoModelFactory;

        public MessagesController(ILogger<MessagesController> logger,
            IMessageService messageService,
            IClienteService clienteService,
            IServicosService servicosService,
            IMessageModelFactory messageModelFactory,
            IClienteModelFactory clienteModelFactory,
            IServicoModelFactory servicoModelFactory
        )
        {
            _logger = logger;
            this._messageService = messageService;
            this._clienteService = clienteService;
            this._servicosService = servicosService;
            this._messageModelFactory = messageModelFactory;
            this._clienteModelFactory = clienteModelFactory;
            this._servicoModelFactory = servicoModelFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(Name = "SendMessages")]
        public async Task<ResponseDto<MessageDTO>> SendMessages([FromBody] RequestDto<MessageDTO> request)
        {
            try
            {
                await _messageService.ImportMessages(
                    _messageModelFactory.ConvertListDtoToListModel(request.Items));

                return new ResponseDto<MessageDTO> 
                {
                    Sucess = true,
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<MessageDTO>
                {
                    Message = ex.Message
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="situacao"></param>
        /// <returns></returns>
        [HttpGet(Name = "GetMessages")]
        public async Task<ResponseDto<MessageDTO>> GetMessages(int? situacao)
        {
            try
            {
                var items = await _messageService.Find(situacao: situacao);

                return new ResponseDto<MessageDTO>
                {
                    Sucess = true,
                    Items = _messageModelFactory.ConvertListModelToListDto(items),
                    Message = situacao.ToString()
                };
            }
            catch
            {
                return new ResponseDto<MessageDTO>
                {
                    Message = "Não foi possivel concluir a operação."
                };
            }
        }


        [HttpGet(Name = "GetCargaInicial")]
        public async Task<ResponseDto<MessageDTO>> GetCargaInicial(int? situacao)
        {
            try
            {
                var clientes = await _clienteService.SearchClientes(new ClienteModel()); ;
                var servicos = await _servicosService.SearchServicos(new ServicoModel());

                var itens = new List<MessageDTO>();

                clientes.ForEach(cliente =>
                {
                    itens.Add(new MessageDTO()
                    {
                        JsonMessage = JsonConvert.SerializeObject(_clienteModelFactory.ConvertModelToDto(cliente)),
                        Situacao = (int)SituacaoMessage.AGUARDANDO_PROCESSAMENTO_MOBILE,
                        TypeMessage = (int)TypeMessage.Cliente
                    });
                });

                servicos.ForEach(servico =>
                {
                    itens.Add(new MessageDTO()
                    {
                        JsonMessage = JsonConvert.SerializeObject(_servicoModelFactory.ConvertModelToDto(servico)),
                        Situacao = (int)SituacaoMessage.AGUARDANDO_PROCESSAMENTO_MOBILE,
                        TypeMessage = (int)TypeMessage.Servico
                    });
                });

                return new ResponseDto<MessageDTO>
                {
                    Sucess = true,
                    Items = itens,
                    Message = situacao.ToString()
                };
            }
            catch
            {
                return new ResponseDto<MessageDTO>
                {
                    Message = "Não foi possivel concluir a operação."
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <retuwrns></returns>
        [HttpGet(Name = "/GenerateCargaMessages")]
        public async Task<ResponseDto<MessageDTO>> GenerateCargaMessages()
        {
            await _clienteService.UpdateCarga();
            await _servicosService.UpdateCarga();
            return new ResponseDto<MessageDTO>();
        }
    }
}