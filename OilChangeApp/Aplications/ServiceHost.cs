using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telegram.Bot.Polling;
using Telegram.Bot.Requests;


public class Host
{
    //public TelegramBotClient client = new TelegramBotClient("8040948589:AAGyIxpxOcm6r0pZwo8VHyoUXmUTi-i18x8");
    private TelegramBotClient _bot;
    public Action<ITelegramBotClient, Update>? OnMessage;

    public Host(string token)
    {
        _bot = new TelegramBotClient(token);
    }

    public async Task Start()
    {
        _bot.StartReceiving(UpdateHandler, ErrorHandler);
        Console.WriteLine("Bot started");
    }

    private async Task ErrorHandler(ITelegramBotClient client, Exception exception, HandleErrorSource arg3, CancellationToken arg4)
    {
        Console.WriteLine("Error: " + exception.Message);
        await Task.CompletedTask;
    }

    private async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken arg3)
    {
        Console.WriteLine("We got Update: " + "(" +update.Message?.Text + ")");
        OnMessage.Invoke(client, update);
        await Task.CompletedTask;
    }
}

