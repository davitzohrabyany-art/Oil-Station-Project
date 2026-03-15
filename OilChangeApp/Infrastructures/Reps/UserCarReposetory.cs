using MySqlConnector;

namespace OilChangeApp.Infrastructures;

public class UserCarReposetory
{
    public static async Task CreatingUserAndCar(int Id, int carId)
    { 
        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();

        var query = "INSERT INTO user_car (User_id, Car_id) VALUES (@Id, @carId)";

        await using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", Id);
        command.Parameters.AddWithValue("@carId", carId);

        await command.ExecuteNonQueryAsync();

    }
}