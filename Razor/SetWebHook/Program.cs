using System;
using Telegram.Bot;

string botToken = "6133479720:AAGTz2zaw5YgVEWqbmSFYgrDk40jst3EKmk";
var telegramBot = new TelegramBotClient(botToken);
await telegramBot.SetWebhookAsync("https://77c6-185-159-162-50.ngrok-free.app/api/bot");
// ngrok
// await telegramBot.SetWebhookAsync("https://generatedByNgrokAddress.ngrok.io/api/bot");
// deployed on Azure
// await telegramBot.SetWebhookAsync("https://DeployedProject.azurewebsites.net/api/bot");
Console.WriteLine("WebHook is set");
Console.WriteLine("To remove the hook press any key");
Console.ReadLine();
// delete Webhook
// await telegramBot.DeleteWebhookAsync();
