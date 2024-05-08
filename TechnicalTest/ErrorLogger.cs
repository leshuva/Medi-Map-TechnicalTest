using Microsoft.Data.SqlClient;

namespace TechnicalTest;

public class ErrorLogger : IErrorLogger
{
    public void LogError(string errorMessage)
    {
        try
        {
            var connectionString = GetDbConnectionString();
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            const string sql = "INSERT INTO dbo.ErrorLog VALUES (@ErrorMessage);";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ErrorMessage", errorMessage);
            command.ExecuteNonQuery();
        }
        catch (SqlException)
        {
        }
    }
    
    private static string GetDbConnectionString()
    {
        var builder = new SqlConnectionStringBuilder
        {
            DataSource = "",
            UserID = "",
            Password = "",
            InitialCatalog = ""
        };

        return builder.ConnectionString;
    }
}