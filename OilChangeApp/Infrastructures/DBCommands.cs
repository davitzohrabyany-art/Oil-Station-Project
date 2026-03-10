using MySqlConnector;

namespace OilChangeApp.resourcesSql;

public class DBCommands
{
    
    public static async Task<bool> DoesUserExists(long tgId)
    {

        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();

        var query = "SELECT COUNT(*) FROM user WHERE Telegram_id = @tgId";

        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@tgId", tgId);

        var result = (long)await command.ExecuteScalarAsync();
        return result > 0;
    }

    public static async Task<bool> DoesUserConnects(string carPassword, string userCarNumber)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();
        var query = "SELECT COUNT(*) FROM car WHERE car_num = @userCarNumber and password = @carPassword";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@userCarNumber", userCarNumber);
        command.Parameters.AddWithValue("@carPassword", carPassword);
        var result = (long)await command.ExecuteScalarAsync();
        return result > 0;
    }

    public static string InfoAboutCar(string userCarNumber)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        connection.Open();
        var query = "SELECT oil_type, car_name FROM car WHERE car_num = @userCarNumber";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@userCarNumber",userCarNumber);
        var reader = command.ExecuteReader();
        if (reader.Read())
        {
            var oil_type = reader["oil_type"].ToString();
            var car_name = reader["car_name"].ToString();
            return oil_type + " " + car_name;
        }

        return "No information about the car";
    }

    public static string InfoAboutOil(string carPassword, string userCarNumber)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        connection.Open();
        var query = "SELECT Id FROM car WHERE car_num = @userCarNumber and password = @carPassword";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@userCarNumber", userCarNumber);
        command.Parameters.AddWithValue("@carPassword", carPassword);
        var carId = (int)command.ExecuteScalar();
        var queryForService = "SELECT Service_id FROM service_visit WHERE Car_id = @carId";
        using var commandForService = new MySqlCommand(queryForService, connection);
        commandForService.Parameters.AddWithValue("@carId", carId);
        var ServiceId = commandForService.ExecuteScalar();
        var queryForOil = "SELECT Oil_name, Oil_location, Oil_liters, Next_change_km, Next_change_date FROM oil_change  WHERE Service_id = @ServiceId";
        using var commandForOil = new MySqlCommand(queryForOil, connection);
        commandForOil.Parameters.AddWithValue("@ServiceId", ServiceId);
        var reader = commandForOil.ExecuteReader();
        if (reader.Read())
        {
            var oil_name = reader["Oil_name"].ToString();
            var oil_location = reader["Oil_location"].ToString();
            var oil_liters = reader["Oil_liters"].ToString();
            var next_change_km = reader["Next_change_km"].ToString();
            var next_change_date = reader["Next_change_date"].ToString();
            return oil_name + " " + oil_location + " " + oil_liters + "liter" + " " + next_change_km + " km" + " " + next_change_date;
        }
        return "No information about the oil";
    }

    public static async Task CreatingUser(int Id, int carId)
    { 
        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();

        var query = "INSERT INTO user_car (user_id, car_id) VALUES (@Id, @carId)";

        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", Id);
        command.Parameters.AddWithValue("@carId", carId);

        await command.ExecuteNonQueryAsync();
    }

    public static int SelectCarId(string carPassword, string userCarNumber)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        connection.Open();
        var query = "SELECT Id FROM car WHERE car_num = @userCarNumber and password = @carPassword";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@userCarNumber", userCarNumber);
        command.Parameters.AddWithValue("@carPassword", carPassword);
        var carId = (int)command.ExecuteScalar();
        return carId;
    }

    public static async Task<int> FindingUserIdFromTgId(long tgId)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();
        var query = "SELECT Id FROM user WHERE Telegram_id = @tgId";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@tgId", tgId);
        var result = await command.ExecuteScalarAsync();
        if (result != null && result != DBNull.Value)
        {
            return int.Parse(result.ToString());
        }
        var insertQuery = "INSERT INTO user (Telegram_id) VALUES (@tgId); SELECT LAST_INSERT_ID();";
        using var insertCommand = new MySqlCommand(insertQuery, connection);
        insertCommand.Parameters.AddWithValue("@tgId", tgId);

        var newId = await insertCommand.ExecuteScalarAsync();
        return int.Parse(newId.ToString());
        
        //Todo Rename Method Names
    }
}