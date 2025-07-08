using Microsoft.Data.SqlClient;

namespace DecoratorPattern.Loggers;

public class SqlLogger : ILogger
{
    private readonly string _connectionString;
    private readonly ILogger _decorated;

    public SqlLogger(string connectionString, ILogger decorated)
    {
        _connectionString = connectionString;
        _decorated = decorated;
    }

    public void Log(string message)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command =
            new SqlCommand("INSERT INTO Logs (Message, LoggedAt) VALUES (@message, @loggedAt)", connection);

        command.Parameters.AddWithValue("@message", message);
        command.Parameters.AddWithValue("@loggedAt", DateTime.Now);

        connection.Open();
        command.ExecuteNonQuery();
        _decorated.Log(message);
    }
}