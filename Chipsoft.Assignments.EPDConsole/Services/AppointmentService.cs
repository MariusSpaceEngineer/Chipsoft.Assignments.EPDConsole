using Chipsoft.Assignments.EPDConsole.Models;
using Chipsoft.Assignments.EPDConsole.Repositories.Interfaces;
using Chipsoft.Assignments.EPDConsole.Services.Interfaces;

namespace Chipsoft.Assignments.EPDConsole.Services
{
    internal class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRespository _appointmentRepository;
        public AppointmentService(IAppointmentRespository appointmentRespository) {
            _appointmentRepository = appointmentRespository;
        }

        public void AddAppointment(Appointment appointment)
        {
            _appointmentRepository.AddAppointment(appointment);
        }

        public IEnumerable<Appointment> GetAllAppointments()
        {
            return _appointmentRepository.GetAllAppointments();
        }
    }
}
