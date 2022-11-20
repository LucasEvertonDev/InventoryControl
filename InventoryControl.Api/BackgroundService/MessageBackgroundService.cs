using InventoryControl.Application.Interfaces;
using InventoryControl.Models.Enums;

namespace InventoryControl.Api.BackService
{
    public class MessageBackgroundService : BackgroundService
    {
        private readonly IMessageService _messageService;

        public MessageBackgroundService(IServiceScopeFactory factory)
        {
            _messageService = factory.CreateScope().ServiceProvider.GetRequiredService<IMessageService>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var messages = await _messageService.Find((int)SituacaoMessage.AGUARDANDO_PROCESSAMENTO_WEB);
            if (messages != null && messages.Any())
            { 
                foreach(var message in messages)
                {
                    _messageService.IntegrateMessage(message);
                }
            }
        }
    }
}
