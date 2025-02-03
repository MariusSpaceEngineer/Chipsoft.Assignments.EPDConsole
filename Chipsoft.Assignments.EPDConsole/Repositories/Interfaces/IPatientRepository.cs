using Chipsoft.Assignments.EPDConsole.Models;

namespace Chipsoft.Assignments.EPDConsole.Repositories.Interfaces
{
    internal interface IPatientRepository
    {
        void AddPatient(Patient patient);
        IEnumerable<Patient> GetAllPatients();
    }
}
