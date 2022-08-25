using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsApp.SimpleCRM.Domain.Contracts.Service.Core;
using WhatsApp.SimpleCRM.Domain.Contracts.Service.Message;
using WhatsApp.SimpleCRM.Domain.Contracts.Service.Parser;

namespace WhatsApp.SimpleCRM.Service.Core
{
    /// <summary>
    /// Serviço de inicialização da aplicação
    /// </summary>
    public class StartupService : IStartupService
    {
        /// <summary>
        /// Serviço de log
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Serviço de mensagens
        /// </summary>
        private readonly IMessageService _messageService;

        /// <summary>
        /// Serviço de decodificação do arquivo
        /// </summary>
        private readonly IParserService _parserService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="logger">Serviço de log</param>
        /// <param name="messageService">Serviço de mensagem</param>
        /// <param name="parserService">Serviço de decodificação de arquivo</param>
        public StartupService(ILogger logger, IMessageService messageService, IParserService parserService)
        {
            _logger = logger;
            _messageService = messageService;
            _parserService = parserService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task SendMessage(string message, string phone)
        {
            List<Tuple<string, string>> communications = new List<Tuple<string, string>>()
            {
                new Tuple<string, string>(phone, message)
            };

            await _messageService.SendBatch(communications);

            _logger.Information("Programa finalizado.");
        }
    }
}
