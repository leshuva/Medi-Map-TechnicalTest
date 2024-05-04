using Microsoft.AspNetCore.Mvc;
using TechnicalTest.ApplicationServices;

namespace TechnicalTest.Controllers;

[ApiController]
[Route("[controller]")]
public class Home(IPatientApplicationService patientApplicationService) : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        var test = patientApplicationService.Test();
        return Ok($"Welcome to Medi-Map Technical Test! This comes from the application service layer: {test}");
    }
}