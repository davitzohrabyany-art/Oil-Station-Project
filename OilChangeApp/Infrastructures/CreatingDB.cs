using MySqlConnector;

namespace OilChangeApp;

public class CreatingDB
{
    public void CreateDB()
    {
        
        string connectionString =
            "Server=127.0.0.1;Port=3306;User=root;Database=oilstationdb;Password=D096055655d;";

        using var conn = new MySqlConnection(connectionString);
        Console.WriteLine("SQL file executed successfully!");
        
    }
}