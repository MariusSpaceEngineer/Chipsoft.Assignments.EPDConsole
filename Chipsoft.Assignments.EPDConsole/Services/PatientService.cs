using Chipsoft.Assignments.EPDConsole.Exceptions;
using Chipsoft.Assignments.EPDConsole.Repositories.Interfaces;
using Chipsoft.Assignments.EPDConsole.Services.Interfaces;

namespace Chipsoft.Assignments.EPDConsole.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public void AddPatient(Patient patient)
        {
            ValidationHelper.ValidateModel(patient);

            // Check if patient with the same name and phone number already exists
            var existingPatient = _patientRepository.GetByNameAndPhoneNumber(patient.Name, patient.PhoneNumber);
            if (existingPatient != null)
            {
                throw new DuplicateEntityException("patient");
            }

            _patientRepository.Add(patient);
        }

        public void DeletePatient(int id)
        {
            _patientRepository.Delete(id);
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _patientRepository.GetAll();
        }
    }
}
