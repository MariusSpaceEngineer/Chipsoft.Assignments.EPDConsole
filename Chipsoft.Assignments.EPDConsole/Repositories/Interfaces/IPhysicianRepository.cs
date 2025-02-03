using Chipsoft.Assignments.EPDConsole.Models;

namespace Chipsoft.Assignments.EPDConsole.Repositories.Interfaces
{
    internal interface IPhysicianRepository
    {
        void AddPhysician(Physician physician);
        void DeletePhysician(int id);
        IEnumerable<Physician> GetAllPhysicians();
        Physician GetPhysicianById(int id);
        Physician GetPhysicianByNameAndSpecialization(string name, string specialization);
    }
}
