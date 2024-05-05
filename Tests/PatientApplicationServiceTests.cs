using Xunit;
using Moq;
using FluentAssertions;
using TechnicalTest.ApplicationServices;
using TechnicalTest.Domain_Services;

namespace Tests;

public class PatientApplicationServiceTests
{
    [Fact]
    public void CheckIfPatientExists_PatientNotInDb_ReturnsFalse()
    {
        // Arrange
        var patientDomainServiceMock = new Mock<IPatientDomainService>();
        var patientApplicationService = new PatientApplicationService(patientDomainServiceMock.Object);
        const int patientId = 1;
        
        // Act
        var isPatientInDb = patientApplicationService.CheckIfPatientExists(patientId);
        
        // Assert
        isPatientInDb.Should().BeFalse();
    }
    
    [Fact]
    public void CheckIfPatientExists_PatientInDb_ReturnsTrue()
    {
        // Arrange
        var patientDomainServiceMock = new Mock<IPatientDomainService>();
        var patientApplicationService = new PatientApplicationService(patientDomainServiceMock.Object);
        const int patientId = 1;

        patientDomainServiceMock.Setup(p => p.IsPatientInDb(patientId)).Returns(true);
        
        // Act
        var isPatientInDb = patientApplicationService.CheckIfPatientExists(patientId);
        
        // Assert
        isPatientInDb.Should().BeTrue();
    }
}