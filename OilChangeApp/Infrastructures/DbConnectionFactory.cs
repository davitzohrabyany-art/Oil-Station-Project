using MySqlConnector;

namespace OilChangeApp.resourcesSql;

public static class DbConnectionFactory
{
    public static readonly string connectionString =
        "Server=127.0.0.1;Port=3306;User=root;Database=oilstationdb;Password=D096055655d;";

    public static MySqlConnection CreateConnection()
        => new MySqlConnection(connectionString);
}