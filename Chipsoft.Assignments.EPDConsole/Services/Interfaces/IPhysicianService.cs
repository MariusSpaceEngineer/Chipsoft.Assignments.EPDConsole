using Chipsoft.Assignments.EPDConsole.Models;

namespace Chipsoft.Assignments.EPDConsole.Services.Interfaces
{
    public interface IPhysicianService
    {
        void AddPhysician(Physician physician);
        void DeletePhysician(int id);
        IEnumerable<Physician> GetAllPhysicians();
    }
}
