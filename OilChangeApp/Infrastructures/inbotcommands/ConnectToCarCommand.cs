 using OilChangeApp.Infrastructures;

 public class ConnectToCarCommand
 {
     public static async Task ConnectToCar(Dictionary<long, string> userStates, Dictionary<long, string> tempPasswords,
         Update update, ITelegramBotClient client)
     {
         var chatId = update.Message?.Chat.Id ?? 0;
         var text = update.Message?.Text ?? "";
         var tgId = update.Message?.From?.Id ?? 0;

         if (text == "/connecttocar")
         {
             userStates[chatId] = "awaiting_password";
             await client.SendMessage(chatId, "Enter car password:");
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

                 var carExists = await UserReposetory.DoesUserConnects(password, carNumber);

                 if (carExists)
                 {
                     try
                     {
                         var id = await UserReposetory.FindingUserIdFromTgId(tgId);
                         var carId = await CarReposetory.SelectCarId(password, carNumber);

                         await UserCarReposetory.CreatingUserAndCar(id, carId);

                         await client.SendMessage(chatId, "Car connected!");
                         await client.SendMessage(chatId, await CarReposetory.SelectFromCar(carNumber));
                         await client.SendMessage(chatId, await OilReposetory.InfoAboutOil(password, carNumber));
                         await client.SendMessage(chatId, "Now you can view your car with /mycars");
                     }
                     catch (Exception ex)
                     {
                         await client.SendMessage(chatId, "Failed to connect car. Please try again later.");
                         Console.WriteLine(ex);
                     }
                 }
                 else
                 {
                     await client.SendMessage(chatId, "Car not found or password is incorrect.");
                 }
                 
                 userStates.Remove(chatId);
                 tempPasswords.Remove(chatId);
                 return;
             }
         }
     }
 }