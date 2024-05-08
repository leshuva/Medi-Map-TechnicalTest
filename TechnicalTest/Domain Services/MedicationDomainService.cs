using System;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TechnicalTest.Domain_Services;

public class MedicationDomainService(IErrorLogger errorLogger, IConfiguration configuration) : IMedicationDomainService
{
    public bool? CheckIfMedicationRecordExists(int patientId)
    {
        var isMedicationRecordInDb = false;
        
        try
        {
            var connectionString = GetDbConnectionString();
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            const string sql = "SELECT * FROM dbo.MedicationAdministration WHERE PatientID = @PatientID";
            
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@PatientID", patientId);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                isMedicationRecordInDb = true;
            }
        }
        catch (SqlException e)
        {
            errorLogger.LogError(e.Message);
            return null;
        }

        return isMedicationRecordInDb;
    }

    public int? CreateMedicationAdministrationRecord(int patientId, decimal patientBmi)
    {
        try
        {
            var connectionString = GetDbConnectionString();
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            const string sql = "INSERT INTO dbo.MedicationAdministration VALUES (@PatientID, @Created, @BMI);" +
                               "SELECT SCOPE_IDENTITY() AS InsertedID;";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@PatientID", patientId);
            command.Parameters.AddWithValue("@Created", DateTime.UtcNow);
            command.Parameters.AddWithValue("@BMI", patientBmi);

            return Convert.ToInt32(command.ExecuteScalar());
        }
        catch (SqlException e)
        {
            errorLogger.LogError(e.Message);
            return null;
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