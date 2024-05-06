using System;
using TechnicalTest.Domain_Services;

namespace TechnicalTest.ApplicationServices;

public class PatientApplicationService(IPatientDomainService patientDomainService) : IPatientApplicationService
{
    public bool CheckIfPatientExists(int patientId)
    {
        return patientDomainService.IsPatientInDb(patientId);
    }

    public decimal CalculateBmi(int patientId)
    {
        var patientDetails = patientDomainService.GetPatientDetailsById(patientId);
        
        // Convert height from centimeters to meters
        var heightInMeters = patientDetails.Height / 100;
        var bmi = patientDetails.Weight / (heightInMeters * heightInMeters);
        bmi = Math.Round(bmi, 2); // Round to 2 decimal places
        
        return bmi;
    }
    
    public int CreatePatientRecord(PatientDetails patientDetails)
    {
        return patientDomainService.AddPatientToDb(patientDetails);
    }
}