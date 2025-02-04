using Chipsoft.Assignments.EPDConsole.Models;

namespace Chipsoft.Assignments.EPDConsole.Repositories.Interfaces
{
    public interface IPhysicianRepository
    {
        void Add(Physician physician);
        void Delete(int id);
        IEnumerable<Physician> GetAll();
        Physician GetById(int id);
        Physician GetByNameAndSpecialization(string name, string specialization);
        bool Exists(int id);
    }
}
