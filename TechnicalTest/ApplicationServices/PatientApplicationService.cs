using System;
using TechnicalTest.Constants;
using TechnicalTest.Domain_Services;
using TechnicalTest.Exceptions;

namespace TechnicalTest.ApplicationServices;

public class PatientApplicationService(IPatientDomainService patientDomainService, IErrorLogger errorLogger) : IPatientApplicationService
{
    public bool CheckIfPatientExists(int patientId)
    {
        var patientAlreadyExists = patientDomainService.CheckIfPatientExists(patientId);
        if (patientAlreadyExists is null)
        {
            errorLogger.LogError(ExceptionMessages.DatabaseErrorMessage);
            throw new DatabaseErrorException(ExceptionMessages.DatabaseErrorMessage);
        } 
        return patientAlreadyExists.Value;
    }

    public decimal CalculateBmi(int patientId)
    {
        var patientDetails = patientDomainService.GetPatientDetailsById(patientId);
        if (patientDetails.PatientId == 0)
        {
            errorLogger.LogError(ExceptionMessages.DatabaseErrorMessage);
            throw new DatabaseErrorException(ExceptionMessages.DatabaseErrorMessage);
        }
        
        // Convert height from centimeters to meters
        var heightInMeters = patientDetails.Height / 100;
        var bmi = patientDetails.Weight / (heightInMeters * heightInMeters);
        bmi = Math.Round(bmi, 2); // Round to 2 decimal places
        
        return bmi;
    }
    
    public int CreatePatientRecord(PatientDetails patientDetails)
    {
        var newlyCreatedPatientId = patientDomainService.CreatePatient(patientDetails);
        if (newlyCreatedPatientId is null)
        {
            errorLogger.LogError(ExceptionMessages.DatabaseErrorMessage);
            throw new DatabaseErrorException(ExceptionMessages.DatabaseErrorMessage);
        }
        return newlyCreatedPatientId.Value;
    }
}