using MySqlConnector;

namespace OilChangeApp.Infrastructures;

public class UserReposetory
{
    public static async Task<int> FindingUserIdFromTgId(long tgId)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();

        var query = "SELECT Id FROM user WHERE Telegram_id = @tgId";
        await using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@tgId", tgId);

        var result = await command.ExecuteScalarAsync();

        if (result != null && result != DBNull.Value)
        {
            return Convert.ToInt32(result);
        }

        var insertQuery = "INSERT INTO user (Telegram_id) VALUES (@tgId);";
        await using var insertCommand = new MySqlCommand(insertQuery, connection);
        insertCommand.Parameters.AddWithValue("@tgId", tgId);

        await insertCommand.ExecuteNonQueryAsync();

        return (int)insertCommand.LastInsertedId;
    }

    public static async Task<int> FindingUserIdFromTgIdWithoutCreating(long tgId)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();
        var query = "SELECT Id FROM user WHERE Telegram_id = @tgId";
        await using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@tgId", tgId);
        var result =  await command.ExecuteScalarAsync();
        if (result == null || result == DBNull.Value)
            return 0;   
        return Convert.ToInt32(result);
    }
    public static async Task<bool> DoesUserConnects(string carPassword, string userCarNumber)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();
        var query = "SELECT COUNT(*) FROM car WHERE car_num = @userCarNumber and password = @carPassword";
        await using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@userCarNumber", userCarNumber);
        command.Parameters.AddWithValue("@carPassword", carPassword);
        var result = (long)await command.ExecuteScalarAsync();
        return result > 0;
    }

    public static async Task<bool> DoesUserExists(long tgId)
    {

        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();

        var query = "SELECT COUNT(*) FROM user WHERE Telegram_id = @tgId";

        await using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@tgId", tgId);
        var result = (long)await command.ExecuteScalarAsync();
        return result > 0;
    }
}