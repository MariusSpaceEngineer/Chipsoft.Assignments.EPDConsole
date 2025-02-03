using Chipsoft.Assignments.EPDConsole.Repositories.Interfaces;
using Chipsoft.Assignments.EPDConsole.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Chipsoft.Assignments.EPDConsole.Services
{
    internal class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRespository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IPhysicianRepository _physicianRepository;

        public AppointmentService(IAppointmentRespository appointmentRepository, IPatientRepository patientRepository, IPhysicianRepository physicianRepository)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _physicianRepository = physicianRepository;
        }

        public void AddAppointment(Appointment appointment)
        {
            // Validate model annotations
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(appointment);
            if (!Validator.TryValidateObject(appointment, validationContext, validationResults, true))
            {
                var errors = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new ArgumentException($"Model validation failed: {errors}");
            }

            // Validate PatientId
            var patient = _patientRepository.GetPatientById(appointment.PatientId);
            if (patient == null)
            {
                throw new ArgumentException("Invalid PatientId.");
            }

            // Validate PhysicianId
            var physician = _physicianRepository.GetPhysicianById(appointment.PhysicianId);
            if (physician == null)
            {
                throw new ArgumentException("Invalid PhysicianId.");
            }

            _appointmentRepository.AddAppointment(appointment);
        }

        public IEnumerable<Appointment> GetAllAppointments()
        {
            return _appointmentRepository.GetAllAppointments();
        }
    }
}
