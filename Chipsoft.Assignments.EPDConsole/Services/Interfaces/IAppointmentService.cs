namespace Chipsoft.Assignments.EPDConsole.Services.Interfaces
{
    public interface IAppointmentService
    {
        void AddAppointment(Appointment appointment);
        IEnumerable<Appointment> GetAllAppointments();
        IEnumerable<Appointment> GetAppointmentsByPatientId(int patientId);
        IEnumerable<Appointment> GetAppointmentsByPhysicianId(int physicianId);
    }
}
