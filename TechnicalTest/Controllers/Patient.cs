using System;
using Microsoft.AspNetCore.Mvc;
using TechnicalTest.ApplicationServices;

namespace TechnicalTest.Controllers;

[ApiController]
[Route("[controller]")]
public class Patient(IPatientApplicationService patientApplicationService) : ControllerBase
{
    [HttpPost]
    public IActionResult CalculateBmi([FromBody] PatientDetails patientDetails)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var patientExists = patientApplicationService.CheckIfPatientExists(patientDetails.PatientId);
        if (patientExists)
        {
            Console.WriteLine("Calculate BMI and create a medication administration record in the MedicationAdministration table.");
        }
        else
        {
            patientApplicationService.CalculateBmi(patientDetails.PatientId);
        }
        return Ok($"Patient exists in db: {patientExists}");
    }
}