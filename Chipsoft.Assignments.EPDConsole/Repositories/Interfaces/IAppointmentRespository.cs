using Chipsoft.Assignments.EPDConsole.Models;

namespace Chipsoft.Assignments.EPDConsole.Repositories.Interfaces
{
    internal interface IAppointmentRespository
    {
        void Add(Appointment appointment);
        IEnumerable<Appointment> GetAll();
    }
}
