using Chipsoft.Assignments.EPDConsole.Models;

namespace Chipsoft.Assignments.EPDConsole.Repositories.Interfaces
{
    internal interface IPhysicianRepository
    {
        void AddPhysician(Physician physician);
        IEnumerable<Patient> GetAllPhysicians();
    }
}
