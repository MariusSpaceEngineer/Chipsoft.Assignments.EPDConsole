namespace Chipsoft.Assignments.EPDConsole.Repositories.Interfaces
{
    public interface IAppointmentRespository
    {
        void Add(Appointment appointment);
        IEnumerable<Appointment> GetAll();
        IEnumerable<Appointment> GetByPatientId(int patientId);
        IEnumerable<Appointment> GetByPhysicianId(int physicianId);
    }
}
