using Chipsoft.Assignments.EPDConsole.Exceptions;
using Chipsoft.Assignments.EPDConsole.Repositories.Interfaces;
using Chipsoft.Assignments.EPDConsole.Services;
using Moq;

namespace Chipsoft.Assignments.EPDConsole.Tests
{
    public class AppointmentServiceTests
    {
        private readonly Mock<IAppointmentRespository> _mockAppointmentRepository;
        private readonly Mock<IPatientRepository> _mockPatientRepository;
        private readonly Mock<IPhysicianRepository> _mockPhysicianRepository;
        private readonly AppointmentService _appointmentService;

        public AppointmentServiceTests()
        {
            _mockAppointmentRepository = new Mock<IAppointmentRespository>();
            _mockPatientRepository = new Mock<IPatientRepository>();
            _mockPhysicianRepository = new Mock<IPhysicianRepository>();
            _appointmentService = new AppointmentService(_mockAppointmentRepository.Object, _mockPatientRepository.Object, _mockPhysicianRepository.Object);
        }

        [Fact]
        public void AddAppointment_ShouldThrowException_WhenPatientDoesNotExist()
        {
            // Arrange
            var appointment = new Appointment
            {
                PatientId = 1,
                PhysicianId = 1,
                Date = DateTime.Now.AddDays(1),
                Description = "Checkup"
            };
            _mockPatientRepository.Setup(repo => repo.Exists(appointment.PatientId)).Returns(false);

            // Act & Assert
            Assert.Throws<NotFoundException>(() => _appointmentService.AddAppointment(appointment));
        }

        [Fact]
        public void AddAppointment_ShouldThrowException_WhenPhysicianDoesNotExist()
        {
            // Arrange
            var appointment = new Appointment
            {
                PatientId = 1,
                PhysicianId = 1,
                Date = DateTime.Now.AddDays(1),
                Description = "Checkup"
            };
            _mockPatientRepository.Setup(repo => repo.Exists(appointment.PatientId)).Returns(true);
            _mockPhysicianRepository.Setup(repo => repo.Exists(appointment.PhysicianId)).Returns(false);

            // Act & Assert
            Assert.Throws<NotFoundException>(() => _appointmentService.AddAppointment(appointment));
        }

        [Fact]
        public void AddAppointment_ShouldAddAppointment_WhenPatientAndPhysicianExist()
        {
            // Arrange
            var appointment = new Appointment
            {
                PatientId = 1,
                PhysicianId = 1,
                Date = DateTime.Now.AddDays(1),
                Description = "Checkup"
            };
            _mockPatientRepository.Setup(repo => repo.Exists(appointment.PatientId)).Returns(true);
            _mockPhysicianRepository.Setup(repo => repo.Exists(appointment.PhysicianId)).Returns(true);

            // Act
            _appointmentService.AddAppointment(appointment);

            // Assert
            _mockAppointmentRepository.Verify(repo => repo.Add(appointment), Times.Once);
        }

        [Fact]
        public void GetAllAppointments_ShouldReturnAllAppointments()
        {
            // Arrange
            var appointments = new List<Appointment>
            {
                new Appointment { PatientId = 1, PhysicianId = 1, Date = DateTime.Now.AddDays(1), Description = "Checkup" },
                new Appointment { PatientId = 2, PhysicianId = 2, Date = DateTime.Now.AddDays(2), Description = "Consultation" }
            };
            _mockAppointmentRepository.Setup(repo => repo.GetAll()).Returns(appointments);

            // Act
            var result = _appointmentService.GetAllAppointments();

            // Assert
            Assert.Equal(appointments, result);
            _mockAppointmentRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public void GetAppointmentsByPatientId_ShouldThrowException_WhenPatientDoesNotExist()
        {
            // Arrange
            int patientId = 1;
            _mockPatientRepository.Setup(repo => repo.Exists(patientId)).Returns(false);

            // Act & Assert
            Assert.Throws<NotFoundException>(() => _appointmentService.GetAppointmentsByPatientId(patientId));
        }

        [Fact]
        public void GetAppointmentsByPatientId_ShouldReturnAppointments_WhenPatientExists()
        {
            // Arrange
            int patientId = 1;
            var appointments = new List<Appointment>
            {
                new Appointment { PatientId = patientId, PhysicianId = 1, Date = DateTime.Now.AddDays(1), Description = "Checkup" },
                new Appointment { PatientId = patientId, PhysicianId = 2, Date = DateTime.Now.AddDays(2), Description = "Consultation" }
            };
            _mockPatientRepository.Setup(repo => repo.Exists(patientId)).Returns(true);
            _mockAppointmentRepository.Setup(repo => repo.GetByPatientId(patientId)).Returns(appointments);

            // Act
            var result = _appointmentService.GetAppointmentsByPatientId(patientId);

            // Assert
            Assert.Equal(appointments, result);
            _mockAppointmentRepository.Verify(repo => repo.GetByPatientId(patientId), Times.Once);
        }

        [Fact]
        public void GetAppointmentsByPhysicianId_ShouldThrowException_WhenPhysicianDoesNotExist()
        {
            // Arrange
            int physicianId = 1;
            _mockPhysicianRepository.Setup(repo => repo.Exists(physicianId)).Returns(false);

            // Act & Assert
            Assert.Throws<NotFoundException>(() => _appointmentService.GetAppointmentsByPhysicianId(physicianId));
        }

        [Fact]
        public void GetAppointmentsByPhysicianId_ShouldReturnAppointments_WhenPhysicianExists()
        {
            // Arrange
            int physicianId = 1;
            var appointments = new List<Appointment>
            {
                new Appointment { PatientId = 1, PhysicianId = physicianId, Date = DateTime.Now.AddDays(1), Description = "Checkup" },
                new Appointment { PatientId = 2, PhysicianId = physicianId, Date = DateTime.Now.AddDays(2), Description = "Consultation" }
            };
            _mockPhysicianRepository.Setup(repo => repo.Exists(physicianId)).Returns(true);
            _mockAppointmentRepository.Setup(repo => repo.GetByPhysicianId(physicianId)).Returns(appointments);

            // Act
            var result = _appointmentService.GetAppointmentsByPhysicianId(physicianId);

            // Assert
            Assert.Equal(appointments, result);
            _mockAppointmentRepository.Verify(repo => repo.GetByPhysicianId(physicianId), Times.Once);
        }
    }
}
