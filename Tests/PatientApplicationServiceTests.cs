using System;
using Xunit;
using Moq;
using FluentAssertions;
using TechnicalTest;
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
    
    [Theory]
    [InlineData(1, 175.9, 65, 21.01)]
    [InlineData(2, 187, 83, 23.74)]
    [InlineData(3, 165.1, 59, 21.65)]
    public void CalculateBmi_MultiplePatients_CalculatesBmi(int patientId, decimal height, decimal weight, decimal expectedResult)
    {
        // Arrange
        var patientDomainServiceMock = new Mock<IPatientDomainService>();
        var patientApplicationService = new PatientApplicationService(patientDomainServiceMock.Object);
        var patientDetails = new PatientDetails
        {
            PatientId = patientId,
            Height = height,
            Weight = weight
        };
        patientDomainServiceMock.Setup(p => p.GetPatientDetailsById(patientId)).Returns(patientDetails);
        
        // Act
        var patientBmi = patientApplicationService.CalculateBmi(patientId);
        
        // Assert
        patientBmi.Should().Be(expectedResult);
    }
    
    [Fact]
    public void CreatePatientRecord_ValidPatientData_ReturnsTrue()
    {
        // Arrange
        var patientDomainServiceMock = new Mock<IPatientDomainService>();
        var patientApplicationService = new PatientApplicationService(patientDomainServiceMock.Object);
        const int newlyCreatedPatientId = 1;
        var patientDetails = new PatientDetails
        {
            FirstName = "James",
            LastName = "Hurren",
            Gender = "Male",
            Dob = DateTime.UtcNow,
            Height = 175,
            Weight = 70
        };
        patientDomainServiceMock.Setup(p => p.AddPatientToDb(patientDetails)).Returns(newlyCreatedPatientId);
        
        // Act
        var patientId = patientApplicationService.CreatePatientRecord(patientDetails);
        
        // Assert
        patientId.Should().Be(newlyCreatedPatientId);
    }
}