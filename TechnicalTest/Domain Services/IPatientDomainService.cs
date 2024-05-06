namespace TechnicalTest.Domain_Services;

public interface IPatientDomainService
{
    /// <summary>
    /// Checks if a patient exists in the database.
    /// </summary>
    /// <param name="patientId">The id of the patient to check.</param>
    /// <returns>True if the patient already exists in the database, false otherwise</returns>
    bool IsPatientInDb(int patientId);

    /// <summary>
    /// Get all details from a patient by id.
    /// </summary>
    /// <param name="patientId">The id of the patient</param>
    /// <returns>A PatientDetails object containing all of the patient details.</returns>
    PatientDetails GetPatientDetailsById(int patientId);

    /// <summary>
    /// Add a new patient to the database.
    /// </summary>
    /// <param name="patientDetails">he PatientDetails class containing the required details to create a new patient record.</param>
    /// <returns>The id of the newly created patient</returns>
    int AddPatientToDb(PatientDetails patientDetails);
}