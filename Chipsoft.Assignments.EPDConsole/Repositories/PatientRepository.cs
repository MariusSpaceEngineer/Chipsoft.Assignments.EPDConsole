using Chipsoft.Assignments.EPDConsole.Models;
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

        public IEnumerable<Patient> GetAllPatients()
        {
            return _context.Patients.ToList();
        }
    }
}
