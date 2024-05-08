using TechnicalTest.Constants;
using TechnicalTest.Domain_Services;
using TechnicalTest.Exceptions;

namespace TechnicalTest.ApplicationServices;

public class MedicationApplicationService(IMedicationDomainService medicationDomainService, IErrorLogger errorLogger) 
    : IMedicationApplicationService
{
    public int CreateMedicationAdministrationRecord(int patientId, decimal patientBmi)
    {
        var medicationRecordAlreadyExists = medicationDomainService.CheckIfMedicationRecordExists(patientId);
        if (medicationRecordAlreadyExists is null)
        {
            errorLogger.LogError(ExceptionMessages.DatabaseErrorMessage);
            throw new DatabaseErrorException(ExceptionMessages.DatabaseErrorMessage);
        } 
        if (medicationRecordAlreadyExists.Value)
        {
            var errorMessage = string.Format(ExceptionMessages.DomainExistsErrorMessage, patientId);
            errorLogger.LogError(errorMessage);
            throw new DomainAlreadyExistsException(errorMessage);
        }
        
        var medicationRecordId = medicationDomainService.CreateMedicationAdministrationRecord(patientId, patientBmi);
        if (medicationRecordId is null)
        {
            errorLogger.LogError(ExceptionMessages.DatabaseErrorMessage);
            throw new DatabaseErrorException(ExceptionMessages.DatabaseErrorMessage);
        }

        return medicationRecordId.Value;
    }
}