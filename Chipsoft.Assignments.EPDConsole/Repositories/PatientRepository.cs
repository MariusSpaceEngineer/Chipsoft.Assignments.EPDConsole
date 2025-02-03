using Chipsoft.Assignments.EPDConsole.Repositories.Interfaces;

namespace Chipsoft.Assignments.EPDConsole.Repositories
{
    internal class PatientRepository : IPatientRepository
    {
        private readonly EPDDbContext _context;
        public PatientRepository(EPDDbContext context)
        {
            _context = context;
        }

        public void AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
        }

        public void DeletePatient(int id)
        {
            _context.Patients.Remove(GetPatientById(id));
            _context.SaveChanges();
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _context.Patients.ToList();
        }

        public Patient GetPatientById(int id)
        {
            return _context.Patients.FirstOrDefault(p => p.Id == id);
        }

        public Patient GetPatientByNameAndPhoneNumber(string name, string phoneNumber)
        {
            return _context.Patients.FirstOrDefault(p => p.Name == name && p.PhoneNumber == phoneNumber);
        }
    }
}
