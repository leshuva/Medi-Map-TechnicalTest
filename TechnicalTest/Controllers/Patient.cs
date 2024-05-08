using Microsoft.AspNetCore.Mvc;
using TechnicalTest.ApplicationServices;

namespace TechnicalTest.Controllers;

[ApiController]
[Route("[controller]")]
public class Patient(IPatientApplicationService patientApplicationService, IMedicationApplicationService medicationApplicationService) : ControllerBase
{
    [HttpPost]
    public IActionResult CalculateBmi([FromBody] PatientDetails patientDetails)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var patientId = patientDetails.PatientId;
        var patientExists = patientApplicationService.CheckIfPatientExists(patientId);
        if (!patientExists)
        {
            patientId = patientApplicationService.CreatePatientRecord(patientDetails);
        }
        
        var bmi = patientApplicationService.CalculateBmi(patientId);
        var recordId = medicationApplicationService.CreateMedicationAdministrationRecord(patientId, bmi);
        return Ok($"Patient BMI calculated: {bmi}. Medication record created with id: {recordId}.");
    }
}