using MySqlConnector;

namespace OilChangeApp.Infrastructures.inbotcommands;

public class BanAndUnBan
{
    public static async Task ChackToBan(MySqlConnection con, Update update, ITelegramBotClient client)
    {
        con.Open();
        var id = await BanedUsersReposetory.SelectFromBanedUsers(con);
        foreach (var VARIABLE in id)
        {
            if (update.Message.From.Id == VARIABLE)
            {
                    
                var expired = await BanedUsersReposetory.SelectExpired(VARIABLE, con);
                if (expired < DateTime.Now)
                {
                    await BanedUsersReposetory.DeleteBanedUser(con, VARIABLE);
                    await client.SendMessage(update.Message.Chat.Id, "You have been unbanned");
                    return;
                }
                await client.SendMessage(update.Message.Chat.Id, "You have been banned");
                //await client.BanChatMember(update.Message.Chat.Id, VARIABLE, expired);
                return;
            }
        }
    }
}