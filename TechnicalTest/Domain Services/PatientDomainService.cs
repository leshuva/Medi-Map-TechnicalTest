using Microsoft.Data.SqlClient;

namespace TechnicalTest.Domain_Services;

public class PatientDomainService : IPatientDomainService
{
    public bool IsPatientInDb(int patientId)
    {
        var isPatientInDb = false;

        try
        {
            var connectionString = GetDbConnectionString();
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            const string sql = "SELECT * FROM dbo.Patient WHERE PatientID = @PatientId";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@PatientId", patientId);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                if (patientId == id)
                {
                    isPatientInDb = true;
                    break;
                }
            }
        }
        catch (SqlException)
        {
            return false;
        }

        return isPatientInDb;
    }

    private static string GetDbConnectionString()
    {
        var builder = new SqlConnectionStringBuilder
        {
            DataSource = "cool-server-in-azure.database.windows.net",
            UserID = "cool-server-in-azure-admin",
            Password = "hKK@$!2Z9NTT",
            InitialCatalog = "medimap-code-test"
        };

        return builder.ConnectionString;
    }
}