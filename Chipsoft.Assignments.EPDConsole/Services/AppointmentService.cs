using Chipsoft.Assignments.EPDConsole.Exceptions;
using Chipsoft.Assignments.EPDConsole.Repositories.Interfaces;
using Chipsoft.Assignments.EPDConsole.Services.Interfaces;

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
            ValidationHelper.ValidateModel(appointment);

            ValidatePatient(appointment.PatientId);
            ValidatePhysician(appointment.PhysicianId);

            _appointmentRepository.Add(appointment);
        }

        private void ValidatePatient(int patientId)
        {
            if (!_patientRepository.Exists(patientId))
            {
                throw new NotFoundException("Patiënt", patientId);
            }
        }

        private void ValidatePhysician(int physicianId)
        {
            if (!_physicianRepository.Exists(physicianId))
            {
                throw new NotFoundException("Arts", physicianId);
            }
        }

        public IEnumerable<Appointment> GetAllAppointments()
        {
            return _appointmentRepository.GetAll();
        }
    }
}
