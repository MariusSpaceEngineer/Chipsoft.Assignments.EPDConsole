namespace Chipsoft.Assignments.EPDConsole.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        void Add(Patient patient);
        void Delete(int id);
        IEnumerable<Patient> GetAll();
        Patient GetById(int id);
        bool Exists(int id);
        Patient GetByNameAndPhoneNumber(string name, string phoneNumber);
    }
}
