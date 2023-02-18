using InventoryControl.Application.Interfaces;
using InventoryControl.Models.Enums;

namespace InventoryControl.Api.BackService
{
    public class MessageBackgroundService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<MessageBackgroundService> _logger;
        private Timer? _timer = null;
        private readonly IMessageService _messageService;

        public MessageBackgroundService(IServiceScopeFactory factory, ILogger<MessageBackgroundService> logger)
        {
            _messageService = factory.CreateScope().ServiceProvider.GetRequiredService<IMessageService>();
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        private void DoWork(object? state)
        {
            var messages = _messageService.Find((int)SituacaoMessage.AGUARDANDO_PROCESSAMENTO_WEB).Result;
            if (messages != null && messages.Any())
            {
                foreach (var message in messages)
                {
                    Task.WaitAll(
                        Task.Run(async () =>
                        {
                            await _messageService.IntegrateMessage(message);
                        })
                    );
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
