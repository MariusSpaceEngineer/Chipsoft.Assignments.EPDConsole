using Chipsoft.Assignments.EPDConsole.Models;

namespace Chipsoft.Assignments.EPDConsole.Repositories.Interfaces
{
    internal interface IAppointmentRespository
    {
        void AddAppointment(Appointment appointment);
        IEnumerable<Appointment> GetAllAppointments();
    }
}
