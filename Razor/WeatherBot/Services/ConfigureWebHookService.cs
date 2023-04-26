using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using WeatherBot.Configuration;

namespace WeatherBot.Services
{
    public class ConfigureWebHookService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly BotConfiguration _botConfiguration;
        private readonly ILogger<ConfigureWebHookService> _logger;
        public ConfigureWebHookService(
            IServiceProvider serviceProvider,
            IOptions<BotConfiguration> botConfigureOptions,
            ILogger<ConfigureWebHookService> logger)
        {
            _serviceProvider = serviceProvider;
            _botConfiguration = botConfigureOptions.Value;
            _logger = logger;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            var botClient = scope.ServiceProvider
                .GetRequiredService<ITelegramBotClient>();
            string webHookAddress = _botConfiguration.HostAddress + _botConfiguration.Route;
            await botClient.SetWebhookAsync(
                url: webHookAddress,
                allowedUpdates: Array.Empty<UpdateType>(),
                cancellationToken: cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            var botClient = scope.ServiceProvider
                .GetRequiredService<ITelegramBotClient>();
            await botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
        }
    }
}
