 using OilChangeApp.Infrastructures;

 public class StartAsAdminCommand
    {
        private static Dictionary<long, string> adminNicknames = new();

        public static async Task StartAsAdmin(
            Dictionary<long, string> userStates,
            Dictionary<long, string> tempPasswords,
            Dictionary<long, string> carNumber,
            Dictionary<long, string> carName,
            Dictionary<long, string> carPassword,
            Dictionary<long, string> oilType,
            Dictionary<long, string> oilLiters,
            Dictionary<long, string> nextChangeKm,
            Dictionary<long, string> oilLocation,
            Dictionary<long, string> nextChangeDate,
            Dictionary<long, string> visit_date,
            ITelegramBotClient client, Update update)
        {
            var chatId = update.Message?.Chat.Id ?? 0;
            var text = update.Message?.Text ?? "";
            if (text == "/startasadmin")
            {
                await client.SendMessage(chatId, "What is your nickname?");
                userStates[chatId] = "WaitingForNickname";
                return;
            }

          
            if (!userStates.ContainsKey(chatId) || !userStates[chatId].StartsWith("WaitingFor"))
                return;

            var currentState = userStates[chatId];

            switch (currentState)
            {
                case "WaitingForNickname":
                    adminNicknames[chatId] = text;
                    userStates[chatId] = "WaitingForPassword";
                    await client.SendMessage(chatId, "Enter password:");
                    return;

                case "WaitingForPassword":
                    var nickname = adminNicknames[chatId];
                    var password = text;
                    var adminExists = await AdminReposetory.DoesAdminExists(nickname, password);
                    if (adminExists)
                    {
                        userStates[chatId] = "WaitingForCarNumber";
                        await client.SendMessage(chatId, "Enter car number:");
                    }
                    else
                    {
                        await client.SendMessage(chatId, "You do not have permission to use this command.");
                        userStates.Remove(chatId);
                        adminNicknames.Remove(chatId);
                    }
                    return;

                case "WaitingForCarNumber":
                    carNumber[chatId] = text;
                    userStates[chatId] = "WaitingForCarName";
                    await client.SendMessage(chatId, "Enter car name:");
                    return;

                case "WaitingForCarName":
                    carName[chatId] = text;
                    userStates[chatId] = "WaitingForCarPassword";
                    await client.SendMessage(chatId, "Enter car password:");
                    return;

                case "WaitingForCarPassword":
                    carPassword[chatId] = text;
                    userStates[chatId] = "WaitingForOilType";
                    await client.SendMessage(chatId, "Enter oil type:");
                    return;

                case "WaitingForOilType":
                    oilType[chatId] = text;
                    userStates[chatId] = "WaitingForOilLiters";
                    await client.SendMessage(chatId, "Enter oil liters:");
                    return;

                case "WaitingForOilLiters":
                    oilLiters[chatId] = text;
                    userStates[chatId] = "WaitingForNextChangeKm";
                    await client.SendMessage(chatId, "Enter next change kilometers:");
                    return;

                case "WaitingForNextChangeKm":
                    nextChangeKm[chatId] = text;
                    userStates[chatId] = "WaitingForOilLocation";
                    await client.SendMessage(chatId, "Enter location of oil:");
                    return;

                case "WaitingForOilLocation":
                    oilLocation[chatId] = text;
                    userStates[chatId] = "WaitingForVisitDate";
                    await client.SendMessage(chatId, "Enter visit date (YYYY-MM-DD):");
                    return;

                case "WaitingForVisitDate":
                    visit_date[chatId] = text;
                    userStates[chatId] = "WaitingForNextChangeDate";
                    await client.SendMessage(chatId, "Enter next change date (YYYY-MM-DD):");
                    return;

                case "WaitingForNextChangeDate":
                    nextChangeDate[chatId] = text;
                    await CarReposetory.InsertCar(carNumber[chatId], carName[chatId], carPassword[chatId], oilType[chatId]);
                    var carId = await CarReposetory.SelectCarIdWithInserting(carPassword[chatId], carNumber[chatId]);
                    var serviceId = await ServiceReposetory.SelectServiceIdByServiceVisiteOrInsert(carId, visit_date[chatId]);
                    await OilReposetory.InsertOil(serviceId, oilType[chatId], oilLiters[chatId], nextChangeKm[chatId], oilLocation[chatId], nextChangeDate[chatId]);

                    await client.SendMessage(chatId, "Car is created successfully!");
                    
                    tempPasswords.Remove(chatId);
                    carName.Remove(chatId);
                    carPassword.Remove(chatId);
                    oilType.Remove(chatId);
                    oilLiters.Remove(chatId);
                    oilLocation.Remove(chatId);
                    nextChangeKm.Remove(chatId);
                    nextChangeDate.Remove(chatId);
                    visit_date.Remove(chatId);
                    adminNicknames.Remove(chatId);
                    userStates.Remove(chatId);
                    return;

                default:
                    await client.SendMessage(chatId, "An unexpected error occurred. Please try again.");
                    userStates.Remove(chatId);
                    adminNicknames.Remove(chatId);
                    return;
            }
        }
    }