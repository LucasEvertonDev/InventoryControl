using InventoryControl.Api.Contracts;
using InventoryControl.Application.Interfaces;
using InventoryControl.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControl.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;
        private readonly IMessageService _messageService;

        public MessagesController(ILogger<MessagesController> logger,
            IMessageService messageService)
        {
            _logger = logger;
            this._messageService = messageService;
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
            catch
            {
                return new ResponseDto<MessageModel>
                {
                    Message = "Não foi possivel concluir a operação."
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
                return new ResponseDto<MessageModel>
                {
                    Sucess = true,
                    Items = await _messageService.Find(situacao: situacao)
                };
            }
            catch
            {
                return new ResponseDto<MessageModel>
                {
                    Message = "Não foi possivel concluir a operação."
                };
            }
        }
    }
}