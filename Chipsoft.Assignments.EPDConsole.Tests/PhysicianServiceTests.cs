using Chipsoft.Assignments.EPDConsole.Exceptions;
using Chipsoft.Assignments.EPDConsole.Models;
using Chipsoft.Assignments.EPDConsole.Repositories.Interfaces;
using Chipsoft.Assignments.EPDConsole.Services;
using Moq;

namespace Chipsoft.Assignments.EPDConsole.Tests
{
    public class PhysicianServiceTests
    {
        private readonly Mock<IPhysicianRepository> _mockPhysicianRepository;
        private readonly PhysicianService _physicianService;

        public PhysicianServiceTests()
        {
            _mockPhysicianRepository = new Mock<IPhysicianRepository>();
            _physicianService = new PhysicianService(_mockPhysicianRepository.Object);
        }

        [Fact]
        public void AddPhysician_ShouldThrowException_WhenPhysicianAlreadyExists()
        {
            // Arrange
            var physician = new Physician
            {
                Name = "Dr. Smith",
                Specialization = "Cardiology",
                Appointments = new List<Appointment>()
            };
            _mockPhysicianRepository.Setup(repo => repo.GetByNameAndSpecialization(physician.Name, physician.Specialization))
                                    .Returns(physician);

            // Act & Assert
            Assert.Throws<DuplicateEntityException>(() => _physicianService.AddPhysician(physician));
        }

        [Fact]
        public void AddPhysician_ShouldAddPhysician_WhenPhysicianDoesNotExist()
        {
            // Arrange
            var physician = new Physician
            {
                Name = "Dr. Smith",
                Specialization = "Cardiology",
                Appointments = new List<Appointment>()
            };
            _mockPhysicianRepository.Setup(repo => repo.GetByNameAndSpecialization(physician.Name, physician.Specialization))
                                    .Returns((Physician)null);

            // Act
            _physicianService.AddPhysician(physician);

            // Assert
            _mockPhysicianRepository.Verify(repo => repo.Add(physician), Times.Once);
        }

        [Fact]
        public void DeletePhysician_ShouldCallDeleteMethodOfRepository()
        {
            // Arrange
            int physicianId = 1;

            // Act
            _physicianService.DeletePhysician(physicianId);

            // Assert
            _mockPhysicianRepository.Verify(repo => repo.Delete(physicianId), Times.Once);
        }

        [Fact]
        public void GetAllPhysicians_ShouldReturnAllPhysicians()
        {
            // Arrange
            var physicians = new List<Physician>
            {
                new Physician { Name = "Dr. Smith", Specialization = "Cardiology", Appointments = new List<Appointment>() },
                new Physician { Name = "Dr. Jones", Specialization = "Neurology", Appointments = new List<Appointment>() }
            };
            _mockPhysicianRepository.Setup(repo => repo.GetAll()).Returns(physicians);

            // Act
            var result = _physicianService.GetAllPhysicians();

            // Assert
            Assert.Equal(physicians, result);
            _mockPhysicianRepository.Verify(repo => repo.GetAll(), Times.Once);
        }
    }
}
