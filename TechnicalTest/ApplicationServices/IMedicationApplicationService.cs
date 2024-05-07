namespace TechnicalTest.ApplicationServices;

public interface IMedicationApplicationService
{
    /// <summary>
    /// Creates a patient administration record.
    /// </summary>
    /// <param name="patientId">The patient id.</param>
    /// <param name="patientBmi">The BMI of the patient.</param>
    /// <returns>The id of the newly created record, or an exception if the record could not be created.</returns>
    /// <exception cref="DatabaseErrorException">Thrown if an error occurs when trying to access the database.</exception>
    /// <exception cref="DomainAlreadyExistsException">Thrown if the patient already has a medication record.</exception>
    int CreateMedicationAdministrationRecord(int patientId, decimal patientBmi);
}