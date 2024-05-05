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
        return Ok($"Patient exists in db: {patientExists}");
    }
}