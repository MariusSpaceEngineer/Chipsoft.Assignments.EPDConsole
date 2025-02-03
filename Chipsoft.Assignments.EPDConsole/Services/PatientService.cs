using Chipsoft.Assignments.EPDConsole.Repositories.Interfaces;
using Chipsoft.Assignments.EPDConsole.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Chipsoft.Assignments.EPDConsole.Services
{
    internal class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public void AddPatient(Patient patient)
        {
            // Validate model annotations
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(patient);
            if (!Validator.TryValidateObject(patient, validationContext, validationResults, true))
            {
                var errors = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new ArgumentException($"Model validation failed: {errors}");
            }

            // Check if patient with the same name and phone number already exists
            var existingPatient = _patientRepository.GetPatientByNameAndPhoneNumber(patient.Name, patient.PhoneNumber);
            if (existingPatient != null)
            {
                throw new ArgumentException("A patient with the same name and phone number already exists.");
            }

            _patientRepository.AddPatient(patient);
        }

        public void DeletePatient(int id)
        {
            _patientRepository.DeletePatient(id);
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _patientRepository.GetAllPatients();
        }
    }
}
