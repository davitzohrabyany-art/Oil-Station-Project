using MySqlConnector;

namespace OilChangeApp.Infrastructures;

public class OilReposetory
{
    public static async Task<string> InfoAboutOil(string carPassword, string userCarNumber)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();

        // Get Car Id
        await using var cmdCar = new MySqlCommand(
            "SELECT Id FROM car WHERE car_num=@userCarNumber AND password=@carPassword", connection);
        cmdCar.Parameters.AddWithValue("@userCarNumber", userCarNumber);
        cmdCar.Parameters.AddWithValue("@carPassword", carPassword);

        var carIdObj = await cmdCar.ExecuteScalarAsync();
        if (carIdObj == null) return "Car not found";
        var carId = Convert.ToInt32(carIdObj);

        // Get Service Id
        await using var cmdService = new MySqlCommand(
            "SELECT Service_id FROM service_visit WHERE Car_id=@carId", connection);
        cmdService.Parameters.AddWithValue("@carId", carId);

        var serviceIdObj = await cmdService.ExecuteScalarAsync();
        if (serviceIdObj == null) return "Service not found";
        var serviceId = Convert.ToInt32(serviceIdObj);

        // Get Oil info
        await using var cmdOil = new MySqlCommand(
            @"SELECT Oil_name, Oil_location, Oil_liters, Next_change_km, Next_change_date
          FROM oil_change WHERE Service_id=@ServiceId", connection);
        cmdOil.Parameters.AddWithValue("@ServiceId", serviceId);

        await using var reader = await cmdOil.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            var oilName = reader["Oil_name"].ToString();
            var oilLocation = reader["Oil_location"].ToString();
            var oilLiters = reader["Oil_liters"].ToString();
            var nextKm = reader["Next_change_km"].ToString();
            var nextDate = reader["Next_change_date"].ToString();
            return $"{oilName} {oilLocation} {oilLiters} liter {nextKm} km {nextDate}";
        }

        return "No information about the oil";
    }
    public static async Task<string> SelectOilByServiceId(int serviceId)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();

        const string query = @"SELECT Oil_name, Oil_location, Oil_liters,
                                  Next_change_km, Next_change_date
                           FROM oil_change
                           WHERE Service_id = @serviceId
                           LIMIT 1";

        await using var command = new MySqlCommand(query, connection);
        command.Parameters.Add("@serviceId", MySqlDbType.Int32).Value = serviceId;

        await using var reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
            return "No information about the oil";

        return $"{reader["Oil_name"]} {reader["Oil_location"]} " +
               $"{reader["Oil_liters"]} liters {reader["Next_change_km"]} km {reader["Next_change_date"]}";
    }

    public static async Task InsertOil(int service_id, string Oil_name, string Oil_liters, string Next_change_km, string Oil_location, string Next_change_date)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await connection.OpenAsync();
        var query = "INSERT INTO oil_change (Service_id, Oil_name, Oil_liters, Next_change_km, Oil_location, Next_change_date)" +
                          "VALUES (@service_id, @Oil_name, @Oil_liters, @Next_change_km, @Oil_location, @Next_change_date)";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.Add("@service_id", MySqlDbType.Int32).Value = service_id;
        command.Parameters.AddWithValue("@Oil_name", Oil_name);
        command.Parameters.Add("@Oil_liters", MySqlDbType.Decimal).Value = Oil_liters;
        command.Parameters.Add("@Next_change_km", MySqlDbType.Decimal).Value = Next_change_km;
        command.Parameters.AddWithValue("@Oil_location", Oil_location);
        command.Parameters.Add("@Next_change_date", MySqlDbType.DateTime).Value = DateTime.Parse(Next_change_date);
        await command.ExecuteNonQueryAsync();
    }
}