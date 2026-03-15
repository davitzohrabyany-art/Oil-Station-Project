using MySqlConnector;

namespace OilChangeApp.Infrastructures;

public class BanedUsersReposetory
{
    public static async Task<List<long>> SelectFromBanedUsers(MySqlConnection connection)
    {
        var bannedIds = new List<long>();
        var query = "SELECT BanedTgId FROM BanedUsers WHERE BanedTgId > 0";
        await using var command = new MySqlCommand(query, connection);
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            bannedIds.Add(reader.GetInt64(0));
        }
        return bannedIds;
    }
    public static async Task<DateTime> SelectExpired(long id, MySqlConnection connection)
    {
        var query = "SELECT ExpiredDate FROM BanedUsers WHERE BanedTgId = @id";
        await using var command = new MySqlCommand(query, connection);
        command.Parameters.Add("@id", MySqlDbType.Int64).Value = id;
        var result = await command.ExecuteScalarAsync();
        if (result != null)
        {
            return (DateTime)result;
        }
        return DateTime.MinValue;
    }

    public static async Task DeleteBanedUser(MySqlConnection connection, long id)
    {
        var query = "DELETE FROM BanedUsers WHERE BanedTgId = @id";
        await using var command = new MySqlCommand(query, connection);
        command.Parameters.Add("@id", MySqlDbType.Int64).Value = id;
        await command.ExecuteNonQueryAsync();
    }
}
