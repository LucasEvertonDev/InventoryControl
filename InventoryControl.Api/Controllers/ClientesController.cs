using InventoryControl.Api.Contracts;
using InventoryControl.Application.Interfaces;
using InventoryControl.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControl.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClientesController : Controller
    {
        private readonly ILogger<MessagesController> _logger;
        private readonly IMessageService _messageService;
        private readonly IClienteService _clienteService;

        public ClientesController(ILogger<MessagesController> logger,
            IMessageService messageService,
            IClienteService clienteService)
        {
            _logger = logger;
            this._messageService = messageService;
            this._clienteService = clienteService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <retuwrns></returns>
        [HttpGet(Name = "GetCargaClientes")]
        public async Task<ResponseDto<MessageModel>> GetCargaClientes()
        {
            await _clienteService.UpdateCarga();
            return new ResponseDto<MessageModel>();
        }
    }
}
