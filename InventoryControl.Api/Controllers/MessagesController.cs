using InventoryControl.Api.Contracts;
using InventoryControl.Application.Interfaces;
using InventoryControl.Models.Entities;
using Microsoft.AspNetCore.Mvc;

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

        public MessagesController(ILogger<MessagesController> logger,
            IMessageService messageService,
            IClienteService clienteService,
            IServicosService servicosService)
        {
            _logger = logger;
            this._messageService = messageService;
            this._clienteService = clienteService;
            this._servicosService = servicosService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(Name = "SendMessages")]
        public async Task<ResponseDto<MessageModel>> SendMessages([FromBody] RequestDto<MessageModel> request)
        {
            try
            {
                await _messageService.ImportMessages(request.Items);

                return new ResponseDto<MessageModel> 
                {
                    Sucess = true,
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<MessageModel>
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
        public async Task<ResponseDto<MessageModel>> GetMessages(int? situacao)
        {
            try
            {
                var items = await _messageService.Find(situacao: situacao);

                return new ResponseDto<MessageModel>
                {
                    Sucess = true,
                    Items = items,
                    Message = situacao.ToString()
                };
            }
            catch
            {
                return new ResponseDto<MessageModel>
                {
                    Message = "N�o foi possivel concluir a opera��o."
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <retuwrns></returns>
        [HttpGet(Name = "/GenerateCargaMessages")]
        public async Task<ResponseDto<MessageModel>> GenerateCargaMessages()
        {
            await _clienteService.UpdateCarga();
            await _servicosService.UpdateCarga();
            return new ResponseDto<MessageModel>();
        }
    }
}