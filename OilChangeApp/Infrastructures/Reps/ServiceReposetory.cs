using MySqlConnector;

namespace OilChangeApp.Infrastructures;

public class ServiceReposetory
{
    public static async Task <int> SelectServiceId(int car_id)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();
        var queryService = "SELECT Service_id FROM service_visit WHERE Car_id = @car_id";
        await using var commandCar = new MySqlCommand(queryService, connection);
        commandCar.Parameters.AddWithValue("@car_id", car_id);
        var result = await commandCar.ExecuteScalarAsync();
        if (result == null || result == DBNull.Value)
            return 0;   // or -1
        return Convert.ToInt32(result);
    }
    public static async Task <int> SelectServiceIdByServiceVisite(int car_id, string visite_date)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();
        var queryService = "SELECT Service_id FROM service_visit WHERE Car_id = @car_id and Visite_date = @visite_date";
        await using var commandCar = new MySqlCommand(queryService, connection);
        commandCar.Parameters.AddWithValue("@car_id", car_id);
        commandCar.Parameters.Add("@visite_date", MySqlDbType.DateTime).Value = DateTime.Parse(visite_date);
        var result = await commandCar.ExecuteScalarAsync();
        if (result == null || result == DBNull.Value)
        {
            return 0;
        }
        return Convert.ToInt32(result);
    }
    public static async Task<int> SelectServiceIdByServiceVisiteOrInsert(int car_id, string visite_date)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();

        var visitDate = DateTime.Parse(visite_date);

        var queryService = @"SELECT Service_id 
                         FROM service_visit 
                         WHERE Car_id = @car_id 
                         AND Visit_date = @visite_date";

        using var commandCar = new MySqlCommand(queryService, connection);
        commandCar.Parameters.AddWithValue("@car_id", car_id);
        commandCar.Parameters.Add("@visite_date", MySqlDbType.DateTime).Value = visitDate;

        var result = await commandCar.ExecuteScalarAsync();

        if (result != null && result != DBNull.Value)
        {
            return Convert.ToInt32(result);
        }

        var insertQuery = @"INSERT INTO service_visit (Car_id, Visit_date)
                        VALUES (@car_id, @visite_date)";

        using var insertcommand = new MySqlCommand(insertQuery, connection);
        insertcommand.Parameters.AddWithValue("@car_id", car_id);
        insertcommand.Parameters.Add("@visite_date", MySqlDbType.DateTime).Value = visitDate;

        await insertcommand.ExecuteNonQueryAsync();

        return (int)insertcommand.LastInsertedId;
    }
}