namespace OilChangeApp.Infrastructures.inbotcommands;

public class MyCarsCommand
{
    public static async Task MyCars(Update update, ITelegramBotClient client)
    {
            var carExcists = await UserReposetory.DoesUserExists(update.Message.From.Id);
            if (carExcists)
            {
                await client.SendMessage(update.Message?.Chat.Id ?? 0, "We found your car");
                var carId = await CarReposetory.SelectCarByTgId(update.Message?.Chat.Id ?? 0 );
                var serviceId = await ServiceReposetory.SelectServiceId(carId);
                await client.SendMessage(update.Message?.Chat.Id ?? 0, await CarReposetory.InfoAboutCar(carId));
                await client.SendMessage(update.Message?.Chat.Id ?? 0, await OilReposetory.SelectOilByServiceId(serviceId));
            }
            else
            {
                await client.SendMessage(update.Message?.Chat.Id ?? 0, "We didn't found your car");
            }
    }
}