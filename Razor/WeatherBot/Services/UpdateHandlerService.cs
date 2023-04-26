using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace WeatherBot.Services
{
    public class UpdateHandlerService
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly ILogger<UpdateHandlerService> _logger;
        public UpdateHandlerService(
            ITelegramBotClient telegramBotClient,
            ILogger<UpdateHandlerService> logger)
        {
            _telegramBotClient = telegramBotClient;
            _logger = logger;
        }

        public Task HandlerErrorAsync(
            Exception ex, CancellationToken cancellationToken)
        {
            string error = ex switch
            {
                ApiRequestException apiRequestException =>
                $"Telegram Error:\n" +
                $"{apiRequestException.Message} " +
                $"{apiRequestException.ErrorCode}",
                _ => ex.ToString();
            };
            _logger.LogError("Error: " + error);
            return Task.CompletedTask;
        }

        public async Task HandlerUpdateAsync(
            Update update,
            CancellationToken cancellationToken)
        {
            Task handler = update switch
            {
                { Message: Message message } => BotOnMessageReceived(message, cancellationToken),
                { EditedMessage: Message message } => BotOnMessageReceived(message, cancellationToken),
                _ => UnknownUpdateHandlerAsync(update, cancellationToken)
                
            };
            await handler;
        }

        private Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Update type is "+ update.Type);
            return Task.CompletedTask;
        }

        private async Task BotOnMessageReceived(Message message, CancellationToken cancellationToken)
        {
            await _telegramBotClient.SendTextMessageAsync(chatId: message.From.Id,
                text: "Heelo" + message.Text);
        }

     
    }
}
