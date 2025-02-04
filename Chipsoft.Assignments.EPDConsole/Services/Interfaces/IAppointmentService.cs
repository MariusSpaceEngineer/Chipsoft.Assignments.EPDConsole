namespace Chipsoft.Assignments.EPDConsole.Services.Interfaces
{
    public interface IAppointmentService
    {
        void AddAppointment(Appointment appointment);
        IEnumerable<Appointment> GetAllAppointments();
    }
}
