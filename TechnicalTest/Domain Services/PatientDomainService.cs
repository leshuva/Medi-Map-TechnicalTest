using Microsoft.Data.SqlClient;

namespace TechnicalTest.Domain_Services;

public class PatientDomainService : IPatientDomainService
{
    public string GetPatientDetails()
    {
        var result = "";
        
        try 
        { 
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "",
                UserID = "",
                Password = "",
                InitialCatalog = ""
            };

            using var connection = new SqlConnection(builder.ConnectionString);
            connection.Open();       

            const string sql = "SELECT * FROM dbo.Patient";

            using var command = new SqlCommand(sql, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var test = reader.GetInt32(0);
                result += $",{test}";
            }
        }
        catch (SqlException e)
        {
            result = e.ToString();
        }
        
        return result;
    }
}