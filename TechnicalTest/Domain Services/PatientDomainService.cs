using System;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TechnicalTest.Domain_Services;

public class PatientDomainService(IErrorLogger errorLogger, IConfiguration configuration) : IPatientDomainService
{
    public bool? CheckIfPatientExists(int patientId)
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
            if (reader.Read())
            {
                isPatientInDb = true;
            }
        }
        catch (SqlException e)
        {
            errorLogger.LogError(e.Message);
            return null;
        }

        return isPatientInDb;
    }

    public PatientDetails GetPatientDetailsById(int patientId)
    {
        var patientDetails = new PatientDetails();

        try
        {
            var connectionString = GetDbConnectionString();
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            const string sql = "SELECT * FROM dbo.Patient WHERE PatientID = @PatientId";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@PatientId", patientId);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                patientDetails.PatientId = reader.GetInt32(0);
                patientDetails.FirstName = reader.GetString(1);
                patientDetails.LastName = reader.GetString(2);
                patientDetails.Gender = reader.GetString(3);
                patientDetails.Dob = reader.GetDateTime(4);
                patientDetails.Height = reader.GetDecimal(5);
                patientDetails.Weight = reader.GetDecimal(6);
            }
        }
        catch (SqlException e)
        {
            errorLogger.LogError(e.Message);
            patientDetails.PatientId = 0;
            return patientDetails;
        }
        
        return patientDetails;
    }

    public int? CreatePatient(PatientDetails patientDetails)
    {
        try
        {
            var connectionString = GetDbConnectionString();
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            const string sql = "INSERT INTO dbo.Patient (FirstName, LastName, Gender, DOB, HeightCms, WeightKgs) " +
                               "VALUES (@FirstName, @LastName, @Gender, @DOB, @HeightCms, @WeightKgs); " +
                               "SELECT SCOPE_IDENTITY() AS InsertedID;";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@FirstName", patientDetails.FirstName);
            command.Parameters.AddWithValue("@LastName", patientDetails.LastName);
            command.Parameters.AddWithValue("@Gender", patientDetails.Gender);
            command.Parameters.AddWithValue("@DOB", patientDetails.Dob);
            command.Parameters.AddWithValue("@HeightCms", patientDetails.Height);
            command.Parameters.AddWithValue("@WeightKgs", patientDetails.Weight);

            return Convert.ToInt32(command.ExecuteScalar()); // Return the id of the newly created patient 
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