using MySqlConnector;

namespace OilChangeApp.Infrastructures;

public class AdminReposetory
{
    public static async Task<string> SelectAdminByNicknameAndPassword(string nickname, string password)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();
        var query = "SELECT Admin_id FROM admins WHERE Nickname = @nickname and Password = @password";
        await using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@nickname", nickname);
        command.Parameters.AddWithValue("@password", password);
        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return reader["Admin_id"].ToString();
        }
        return null;
    }

    public static async Task<long> RememberAdmin(MySqlConnection con, string nickname, string password)
    {
        const string query = "SELECT TgId FROM admins WHERE nickname = @nickname and password = @password";
        await using var command = new MySqlCommand(query, con);
        command.Parameters.AddWithValue("@nickname", nickname);
        command.Parameters.AddWithValue("@password", password);
        var result = await command.ExecuteScalarAsync();
        return Convert.ToInt64(result);
    }

    public static async Task<bool> DoesAdminExists(MySqlConnection con, long adminTgId)
    {
        const string query = "SELECT * FROM admins WHERE TgId = @adminTgId";
        await using var command = new MySqlCommand(query, con);
        command.Parameters.AddWithValue("@adminTgId", adminTgId);
        var result = await command.ExecuteScalarAsync();
        return Convert.ToBoolean(result);
    }

    public static async Task<bool> DoesAdminExistsByNicknameAndPassword(MySqlConnection connection, string nickname, string password)
    {
        var query = "SELECT Admin_id FROM admins WHERE nickname = @nickname and password = @password";
        await using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@nickname", nickname);
        command.Parameters.AddWithValue("@password", password);
        var result = await command.ExecuteScalarAsync();
        if (result == null || result == DBNull.Value)
            return false;
        return (int)result > 0;
    }

    public static async Task CreateAdmin(MySqlConnection con, string nickname, string password, long tgId)
    {
        var query = "Insert into admins (nickname, password, tgId) values (@nickname, @password, @tgId)";
        await using var command = new MySqlCommand(query, con);
        command.Parameters.AddWithValue("@nickname", nickname);
        command.Parameters.AddWithValue("@password", password);
        command.Parameters.AddWithValue("@tgId", tgId);
        await command.ExecuteNonQueryAsync();
    }
}