using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TechnicalTest;

public class ErrorLogger(IConfiguration configuration) : IErrorLogger
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
    
    private string GetDbConnectionString()
    {
        var builder = new SqlConnectionStringBuilder
        {
            DataSource = configuration["DatabaseConnection:DataSource"],
            UserID = configuration["DatabaseConnection:UserID"],
            Password = configuration["DatabaseConnection:Password"],
            InitialCatalog = configuration["DatabaseConnection:InitialCatalog"]
        };

        return builder.ConnectionString;
    }
}