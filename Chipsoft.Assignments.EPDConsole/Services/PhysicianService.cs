using Chipsoft.Assignments.EPDConsole.Models;
using Chipsoft.Assignments.EPDConsole.Repositories.Interfaces;
using Chipsoft.Assignments.EPDConsole.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

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
            // Validate model annotations
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(physician);
            if (!Validator.TryValidateObject(physician, validationContext, validationResults, true))
            {
                var errors = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new ArgumentException($"Model validation failed: {errors}");
            }

            // Check if physician with the same name and phone number already exists
            var existingPhysician = _physicianRepository.GetPhysicianByNameAndSpecialization(physician.Name, physician.Specialization);
            if (existingPhysician != null)
            {
                throw new ArgumentException("A physician with the same name and specialization already exists.");
            }

            _physicianRepository.AddPhysician(physician);
        }

        public void DeletePhysician(int id)
        {
            _physicianRepository.DeletePhysician(id);
        }

        public IEnumerable<Physician> GetAllPhysicians()
        {
            return _physicianRepository.GetAllPhysicians();
        }
    }
}
