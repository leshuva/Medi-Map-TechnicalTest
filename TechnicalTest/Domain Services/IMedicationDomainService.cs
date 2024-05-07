namespace TechnicalTest.Domain_Services;

public interface IMedicationDomainService
{
    /// <summary>
    /// Checks if a medication administration record already exists in the database.
    /// </summary>
    /// <param name="patientId">The id of the patient.</param>
    /// <returns>True if medication record for the given patient exists, false otherwise, or null if a database error occurs.</returns>
    bool? CheckIfMedicationRecordExists(int patientId);
    
    /// <summary>
    /// Creates a new medication administration record.
    /// </summary>
    /// <param name="patientId">The patient id.</param>
    /// <param name="patientBmi">The patient BMI.</param>
    /// <returns>The id of the newly created medication administration record, or null if a database error occurs</returns>
    int? CreateMedicationAdministrationRecord(int patientId, decimal patientBmi);
}