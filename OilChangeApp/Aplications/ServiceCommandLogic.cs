
namespace OilChangeApp.Aplications;


public class CommandLogic
{
    static async Task<string> GotMessage(ITelegramBotClient client, Update update)
    {
        await client.SendMessage(update.Message?.Chat.Id ?? 0, "Hi enter your password");
        string message = update.Message?.Text ?? "";
        await Task.Yield();
        return message;
    }
    static Dictionary<long, string> userStates = new();
    static Dictionary<long, string> tempPasswords = new();

    public static async void OnMessage(ITelegramBotClient client, Update update)
    {
        if(update.Message?.Text == "/startasadmin")
        {
            await client.SendMessage(update.Message?.Chat.Id ?? 0, "Enter password");
        }
        var chatId = update.Message?.Chat.Id ?? 0;
        var text = update.Message?.Text ?? "";
        
        if (text == "/connectToCar")
        {
            userStates[chatId] = "awaiting_password";
            await client.SendMessage(chatId, "Enter password:");
            return;
        }

        if (userStates.ContainsKey(chatId))
        {
            if (userStates[chatId] == "awaiting_password")
            {
                tempPasswords[chatId] = text;
                userStates[chatId] = "awaiting_car_number";
                await client.SendMessage(chatId, "Enter car number:");
                return;
            }

            if (userStates[chatId] == "awaiting_car_number")
            {
                var password = tempPasswords[chatId];
                var carNumber = text;

                var carExists = await DBCommands.DoesUserConnects(password, carNumber);

                if (carExists)
                {
                    await client.SendMessage(chatId, "Car connected!");
                    await client.SendMessage(chatId, text: DBCommands.InfoAboutCar(carNumber));
                    await client.SendMessage(chatId, text: DBCommands.InfoAboutOil(password, carNumber));
                    await client.SendMessage(chatId, "if you want to save your username /SaveUsername");
                }

                else
                {
                    await client.SendMessage(chatId, "Wrong password or car number");
                }


                userStates.Remove(chatId);
                tempPasswords.Remove(chatId);
                return;
            }
        }
        

        
        if(update.Message?.Text == "/mycars")
        {
            var carExcists = await DBCommands.UserExists(update.Message.From.Id);
            if (carExcists)
            {
                await client.SendMessage(update.Message?.Chat.Id ?? 0, "We found your car");
            }
        }
    }
    
}