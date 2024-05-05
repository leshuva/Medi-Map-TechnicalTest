namespace TechnicalTest.ApplicationServices;

public interface IPatientApplicationService
{
    /// <summary>
    /// Checks if a patient already exists in the database.
    /// </summary>
    /// <param name="patientId">The id of the patient to check.</param>
    /// <returns>True if the patient already exists in the database, false otherwise</returns>
    bool CheckIfPatientExists(int patientId);

    /// <summary>
    /// Calculates and returns the BMI (Body mass index) of the given patient.
    /// </summary>
    /// <param name="patientId">The id of the patient.</param>
    /// <returns>The calculated BMI of the patient.</returns>
    decimal CalculateBmi(int patientId);
}