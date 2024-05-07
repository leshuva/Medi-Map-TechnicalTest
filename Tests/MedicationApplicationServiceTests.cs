using System;
using Xunit;
using Moq;
using FluentAssertions;
using TechnicalTest.ApplicationServices;
using TechnicalTest.Constants;
using TechnicalTest.Domain_Services;
using TechnicalTest.Exceptions;

namespace Tests;

public class MedicationApplicationServiceTests
{
    [Fact]
    public void CreateMedicationAdministrationRecord_DatabaseError_ThrowsDatabaseErrorException()
    {
        // Arrange
        var medicationDomainServiceMock = new Mock<IMedicationDomainService>();
        var medicationApplicationService = new MedicationApplicationService(medicationDomainServiceMock.Object);
        
        // Act
        Action action = () =>
            medicationApplicationService.CreateMedicationAdministrationRecord(It.IsAny<int>(), It.IsAny<decimal>());
        
        // Act - Assert
        action.Should().Throw<DatabaseErrorException>().WithMessage(ExceptionMessages.DatabaseErrorMessage);
    }
    
    [Fact]
    public void CreateMedicationAdministrationRecord_RecordAlreadyExists_ThrowsDomainAlreadyExistsException()
    {
        // Arrange
        var medicationDomainServiceMock = new Mock<IMedicationDomainService>();
        var medicationApplicationService = new MedicationApplicationService(medicationDomainServiceMock.Object);
        const int patientId = 1;
        medicationDomainServiceMock.Setup(m => m.CheckIfMedicationRecordExists(patientId))
            .Returns(true);
        
        // Act
        Action action = () =>
            medicationApplicationService.CreateMedicationAdministrationRecord(patientId, It.IsAny<decimal>());
        
        // Act - Assert
        action.Should().Throw<DomainAlreadyExistsException>().WithMessage(string.Format(ExceptionMessages.DomainExistsErrorMessage, patientId));
    }
    
    [Fact]
    public void CreateMedicationAdministrationRecord_NoErrors_MedicationRecordCreated()
    {
        // Arrange
        var medicationDomainServiceMock = new Mock<IMedicationDomainService>();
        var medicationApplicationService = new MedicationApplicationService(medicationDomainServiceMock.Object);
        const int patientId = 1;
        const int medicationRecordId = 1;
        medicationDomainServiceMock.Setup(m => m.CheckIfMedicationRecordExists(patientId))
            .Returns(false);
        medicationDomainServiceMock.Setup(m =>
            m.CreateMedicationAdministrationRecord(It.IsAny<int>(), It.IsAny<decimal>()))
            .Returns(medicationRecordId);
        
        // Act
        var newRecordId = medicationApplicationService.CreateMedicationAdministrationRecord(patientId, (decimal) 20.7);
        
        // Assert
        newRecordId.Should().Be(medicationRecordId);
    }
}