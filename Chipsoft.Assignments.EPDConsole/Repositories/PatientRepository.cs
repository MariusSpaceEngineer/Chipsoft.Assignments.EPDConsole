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

        public void Add(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Patients.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IEnumerable<Patient> GetAll()
        {
            return _context.Patients.ToList();
        }

        public Patient GetById(int id)
        {
            return _context.Patients.FirstOrDefault(p => p.Id == id);
        }

        public Patient GetByNameAndPhoneNumber(string name, string phoneNumber)
        {
            return _context.Patients.FirstOrDefault(p => p.Name == name && p.PhoneNumber == phoneNumber);
        }

        public bool Exists(int id)
        {
            return _context.Patients.Any(p => p.Id == id);
        }
    }
}
