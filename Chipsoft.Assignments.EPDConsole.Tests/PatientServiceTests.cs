using Chipsoft.Assignments.EPDConsole.Exceptions;
using Chipsoft.Assignments.EPDConsole.Repositories.Interfaces;
using Chipsoft.Assignments.EPDConsole.Services;
using Moq;

namespace Chipsoft.Assignments.EPDConsole.Tests
{
    public class PatientServiceTests
    {
        private readonly Mock<IPatientRepository> _mockPatientRepository;
        private readonly PatientService _patientService;

        public PatientServiceTests()
        {
            _mockPatientRepository = new Mock<IPatientRepository>();
            _patientService = new PatientService(_mockPatientRepository.Object);
        }

        [Fact]
        public void AddPatient_ShouldThrowException_WhenPatientAlreadyExists()
        {
            // Arrange
            var patient = new Patient
            {
                Name = "John Doe",
                Address = "123 Main St",
                PhoneNumber = "1234567890",
                Email = "john.doe@example.com",
            };
            _mockPatientRepository.Setup(repo => repo.GetByNameAndPhoneNumber(patient.Name, patient.PhoneNumber))
                                  .Returns(patient);

            // Act & Assert
            Assert.Throws<DuplicateEntityException>(() => _patientService.AddPatient(patient));
        }

        [Fact]
        public void AddPatient_ShouldAddPatient_WhenPatientDoesNotExist()
        {
            // Arrange
            var patient = new Patient
            {
                Name = "John Doe",
                Address = "123 Main St",
                PhoneNumber = "1234567890",
                Email = "john.doe@example.com",
            };
            _mockPatientRepository.Setup(repo => repo.GetByNameAndPhoneNumber(patient.Name, patient.PhoneNumber))
                                  .Returns((Patient)null);

            // Act
            _patientService.AddPatient(patient);

            // Assert
            _mockPatientRepository.Verify(repo => repo.Add(patient), Times.Once);
        }

        [Fact]
        public void DeletePatient_ShouldCallDeleteMethodOfRepository()
        {
            // Arrange
            int patientId = 1;

            // Act
            _patientService.DeletePatient(patientId);

            // Assert
            _mockPatientRepository.Verify(repo => repo.Delete(patientId), Times.Once);
        }

        [Fact]
        public void GetAllPatients_ShouldReturnAllPatients()
        {
            // Arrange
            var patients = new List<Patient>
            {
                new Patient { Name = "John Doe", PhoneNumber = "1234567890" },
                new Patient { Name = "Jane Doe", PhoneNumber = "0987654321" }
            };
            _mockPatientRepository.Setup(repo => repo.GetAll()).Returns(patients);

            // Act
            var result = _patientService.GetAllPatients();

            // Assert
            Assert.Equal(patients, result);
            _mockPatientRepository.Verify(repo => repo.GetAll(), Times.Once);
        }
    }
}
