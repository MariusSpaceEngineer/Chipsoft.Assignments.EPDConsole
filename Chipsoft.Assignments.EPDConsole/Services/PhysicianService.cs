using Chipsoft.Assignments.EPDConsole.Models;
using Chipsoft.Assignments.EPDConsole.Repositories.Interfaces;
using Chipsoft.Assignments.EPDConsole.Services.Interfaces;

namespace Chipsoft.Assignments.EPDConsole.Services
{
    internal class PhysicianService: IPhysicianService
    {
        private readonly IPhysicianRepository _physicianRepository;
        public PhysicianService(IPhysicianRepository physicianRepository)
        {
            _physicianRepository = physicianRepository;
        }

        public void AddPhysician(Physician physician)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Physician> GetAllPhysicians()
        {
            return _physicianRepository.GetAllPhysicians();
        }
    }
}
