using TechnicalTest.Domain_Services;

namespace TechnicalTest.ApplicationServices;

public class PatientApplicationService(IPatientDomainService patientDomainService) : IPatientApplicationService
{
    public bool CheckIfPatientExists(int patientId)
    {
        return patientDomainService.IsPatientInDb(patientId);
    }
}