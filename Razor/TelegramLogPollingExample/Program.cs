using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

string token = "6193801115:AAF2rs9vZbziiZBeYMXYslVl1gcKexcLXTw";
TelegramBotClient botClient = new TelegramBotClient(token);
var user = await botClient.GetMeAsync();
Console.WriteLine($"Id: {user.Id}\n{user.Username}");
using CancellationTokenSource cts = new CancellationTokenSource();
ReceiverOptions receiverOptions = new ReceiverOptions
{
    AllowedUpdates = Array.Empty<UpdateType>(),
};
botClient.StartReceiving(updateHandler: UpdateHandlerAsync, 
   pollingErrorHandler: PollingErrorHandlerAsync, cancellationToken: cts.Token, receiverOptions: receiverOptions);
Console.ReadLine();
cts.Cancel();

Task PollingErrorHandlerAsync(ITelegramBotClient botClient, Exception ex, CancellationToken ct)
{
    var error = ex switch
    {
        ApiRequestException requestException=>
        $"Error:\n"+
        $"{requestException.Message}\n"+
        $"{requestException.ErrorCode}"+
        $"{requestException.Data}",
        _ => ex.ToString(),
    };
    Console.WriteLine(error);
    return Task.CompletedTask;
}

async Task UpdateHandlerAsync(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken ct)
{
    if(update.Message is not Message message)
    { return; }
    if(message.Text is string text)
    {
        long chatId = message.Chat.Id;
        await botClient.SendTextMessageAsync(chatId: chatId, text: "You wrote: "+text, cancellationToken: ct);
    }
    if(message.Document is  Document document)
    {
        long chatId = message.Chat.Id;
        await botClient.SendTextMessageAsync(chatId: chatId, text: "Here your doc: ", cancellationToken: ct);
        await botClient.ForwardMessageAsync(
            chatId,
            chatId,
            message.MessageId,
            cancellationToken: ct
            );
      
    }
}