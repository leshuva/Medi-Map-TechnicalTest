using TechnicalTest.Domain_Services;

namespace TechnicalTest.ApplicationServices;

public class PatientApplicationService(IPatientDomainService patientDomainService) : IPatientApplicationService
{
    public string Test()
    {
        return patientDomainService.GetPatientDetails();
    }
}