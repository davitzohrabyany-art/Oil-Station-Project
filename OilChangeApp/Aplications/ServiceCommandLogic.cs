
using OilChangeApp.Domain.Entities;
using OilChangeApp.Infrastructures;
using OilChangeApp.Infrastructures.inbotcommands;

namespace OilChangeApp.Aplications;


public class CommandLogic
    {
        private static Dictionary<long, string> userStates = new();
        private static Dictionary<long, string> userStatesForConnection = new();
        private static Dictionary<long, string> tempPasswords = new();
        private static Dictionary<long, string> carNumber = new();
        private static Dictionary<long, string> carName = new();
        private static Dictionary<long, string> carPassword = new();
        private static Dictionary<long, string> oilType = new();
        private static Dictionary<long, string> oilLiters = new();
        private static Dictionary<long, string> nextChangeKm = new();
        private static Dictionary<long, string> oilLocation = new();
        private static Dictionary<long, string> nextChangeDate = new();
        private static Dictionary<long, string> visit_date = new();

        private static async Task RouteCommands(Update update, ITelegramBotClient client)
        {
            var text = update.Message?.Text ?? "";

            if (text == "/start")
            {
                await StartCommand.Start(text, client, update);
                return;
            }

            if (text == "/connecttocar" || userStatesForConnection.ContainsKey(update.Message.Chat.Id))
            {
                await ConnectToCarCommand.ConnectToCar(userStatesForConnection, tempPasswords, update, client);
                return;
            }

            if (text.StartsWith("/mycars"))
            {
                await MyCarsCommand.MyCars(update, client);
                return;
            }

            if (text == "/startasadmin" || (userStates.ContainsKey(update.Message.Chat.Id) && userStates[update.Message.Chat.Id].StartsWith("WaitingFor")))
            {
                await StartAsAdminCommand.StartAsAdmin(
                    userStates, tempPasswords, carNumber, carName, carPassword,
                    oilType, oilLiters, nextChangeKm, oilLocation,
                    nextChangeDate, visit_date, client, update);
                return;
            }
        }

        public static async void OnMessage(ITelegramBotClient client, Update update)
        {
            await RouteCommands(update, client);
        }
    }
