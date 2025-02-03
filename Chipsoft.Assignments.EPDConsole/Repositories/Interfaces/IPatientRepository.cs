namespace Chipsoft.Assignments.EPDConsole.Repositories.Interfaces
{
    internal interface IPatientRepository
    {
        void AddPatient(Patient patient);
        void DeletePatient(int id);
        IEnumerable<Patient> GetAllPatients();
        Patient GetPatientById(int id);
        Patient GetPatientByNameAndPhoneNumber(string name, string phoneNumber);
    }
}
