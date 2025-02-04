namespace Chipsoft.Assignments.EPDConsole.Repositories.Interfaces
{
    public interface IAppointmentRespository
    {
        void Add(Appointment appointment);
        IEnumerable<Appointment> GetAll();
    }
}
