using Chipsoft.Assignments.EPDConsole.Models;

namespace Chipsoft.Assignments.EPDConsole.Services.Interfaces
{
    public interface IAppointmentService
    {
        void AddAppointment(Appointment appointment);
        IEnumerable<Appointment> GetAllAppointments();
    }
}
