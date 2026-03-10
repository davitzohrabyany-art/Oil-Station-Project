namespace OilChangeApp.Infrastructures.inbotcommands;

public class StartCommand
{
    public static async Task Start(string text, ITelegramBotClient client, Update update)
    {
        await client.SendMessage(update.Message?.Chat.Id ?? 0, "Hi how can I help you?");
    }
}