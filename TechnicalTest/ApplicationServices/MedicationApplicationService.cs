using TechnicalTest.Constants;
using TechnicalTest.Domain_Services;
using TechnicalTest.Exceptions;

namespace TechnicalTest.ApplicationServices;

public class MedicationApplicationService(IMedicationDomainService medicationDomainService) : IMedicationApplicationService
{
    public int CreateMedicationAdministrationRecord(int patientId, decimal patientBmi)
    {
        var medicationRecordAlreadyExists = medicationDomainService.CheckIfMedicationRecordExists(patientId);
        if (medicationRecordAlreadyExists is null)
        {
            throw new DatabaseErrorException(ExceptionMessages.DatabaseErrorMessage);
        } 
        if (medicationRecordAlreadyExists.Value)
        {
            throw new DomainAlreadyExistsException(string.Format(ExceptionMessages.DomainExistsErrorMessage, patientId));
        }
        
        var medicationRecordId = medicationDomainService.CreateMedicationAdministrationRecord(patientId, patientBmi);
        if (medicationRecordId is null)
        {
            throw new DatabaseErrorException(ExceptionMessages.DatabaseErrorMessage);
        }

        return medicationRecordId.Value;
    }
}