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

    public static async Task<bool> DoesAdminExists(string nickname, string password)
    {
        var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();
        var query = "SELECT Admin_id FROM admins WHERE nickname = @nickname and password = @password";
        await using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@nickname", nickname);
        command.Parameters.AddWithValue("@password", password);
        var result = await command.ExecuteScalarAsync();
        if (result == null || result == DBNull.Value)
            return false;
        return (int)result > 0;
    }
}