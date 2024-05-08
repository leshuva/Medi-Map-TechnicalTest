namespace TechnicalTest.Domain_Services;

public interface IPatientDomainService
{
    /// <summary>
    /// Checks if a patient already exists in the database.
    /// </summary>
    /// <param name="patientId">The id of the patient.</param>
    /// <returns>True if the patient already exists, false otherwise, or null if a database error occurs.</returns>
    bool? CheckIfPatientExists(int patientId);

    /// <summary>
    /// Get all details from a patient by id.
    /// </summary>
    /// <param name="patientId">The id of the patient.</param>
    /// <returns>A PatientDetails object containing all the patient details, or a PatientDetails object with an id of
    /// 0 if a database error occurs. </returns>
    PatientDetails GetPatientDetailsById(int patientId);

    /// <summary>
    /// Creates a new patient.
    /// </summary>
    /// <param name="patientDetails">A PatientDetails object containing the required details to create a new patient.</param>
    /// <returns>The id of the newly created patient, or null if a database error occurs.</returns>
    int? CreatePatient(PatientDetails patientDetails);
}