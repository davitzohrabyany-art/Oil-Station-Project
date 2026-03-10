using MySqlConnector;

namespace OilChangeApp.Infrastructures;

public class CarReposetory
{
    public static async Task<string> SelectFromCar(string userCarNumber)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();
        var query = "SELECT oil_type, car_name FROM car WHERE car_num = @userCarNumber";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@userCarNumber",userCarNumber);
        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            var oil_type = reader["oil_type"].ToString();
            var car_name = reader["car_name"].ToString();
            return oil_type + " " + car_name;
        }

        return "No information about the car";
    }

    public static async Task<int> SelectCarId(string carPassword, string userCarNumber)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();

        var query = "SELECT Id FROM car WHERE car_num = @userCarNumber AND password = @carPassword";

        await using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@userCarNumber", userCarNumber);
        command.Parameters.AddWithValue("@carPassword", carPassword);

        var result = await command.ExecuteScalarAsync();
        if (result != null)
        {
            return (int)result;
        }
        return -1;
    }

    public static async Task<int> SelectCarIdWithInserting(string carPassword, string userCarNumber)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();

        var query = "SELECT Id FROM car WHERE car_num = @userCarNumber AND password = @carPassword";

        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@userCarNumber", userCarNumber);
        command.Parameters.AddWithValue("@carPassword", carPassword);

        var result = await command.ExecuteScalarAsync();

        if (result != null)
        {
            return Convert.ToInt32(result);
        }
        var insertQuery = @"INSERT INTO car (car_num, password)
                            VALUES (@userCarNumber, @carPassword);";

        await using var insertCommand = new MySqlCommand(insertQuery, connection);
        insertCommand.Parameters.AddWithValue("@userCarNumber", userCarNumber);
        insertCommand.Parameters.AddWithValue("@carPassword", carPassword);

        insertCommand.ExecuteNonQuery();
        return (int)insertCommand.LastInsertedId;
    }
    public static async Task<int> SelectCarByTgId(long tgId)
    {
        var user_id = await UserReposetory.FindingUserIdFromTgIdWithoutCreating(tgId);
        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();

        var query = "SELECT Car_id FROM user_car WHERE User_id = @user_id";
        await using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@User_id", user_id);
        var result = await command.ExecuteScalarAsync();
        if (result == null || result == DBNull.Value)
            return 0;
        return Convert.ToInt32(result);
    }

    public static async Task InsertCar(string car_num, string car_name, string password, string oil_type)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();
        var query =
            "INSERT INTO car (car_num, car_name, password, oil_type) " +
            "VALUES (@car_num, @car_name, @password, @oil_type)";
        await using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@car_num", car_num);
        command.Parameters.AddWithValue("@car_name", car_name);
        command.Parameters.AddWithValue("@password", password);
        command.Parameters.AddWithValue("@oil_type", oil_type);
        await command.ExecuteNonQueryAsync();
    }

    public static async Task<string> InfoAboutCar(int car_id)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();
        var query = "SELECT Car_num, Car_name FROM car WHERE Id = @car_id";
        await using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@car_id", car_id);
        var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return reader["Car_num"].ToString() + " " + reader["Car_name"].ToString();
             
        }
        return "No information about the car";
    }

}