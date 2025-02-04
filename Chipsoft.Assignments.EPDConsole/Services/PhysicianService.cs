using Chipsoft.Assignments.EPDConsole.Exceptions;
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
            ValidationHelper.ValidateModel(physician);

            // Check if physician with the same name and phone number already exists
            var existingPhysician = _physicianRepository.GetByNameAndSpecialization(physician.Name, physician.Specialization);
            if (existingPhysician != null)
            {
                throw new DuplicateEntityException("physician");
            }

            _physicianRepository.Add(physician);
        }

        public void DeletePhysician(int id)
        {
            _physicianRepository.Delete(id);
        }

        public IEnumerable<Physician> GetAllPhysicians()
        {
            return _physicianRepository.GetAll();
        }
    }
}
