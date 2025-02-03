using Chipsoft.Assignments.EPDConsole.Models;
using Chipsoft.Assignments.EPDConsole.Repositories.Interfaces;

namespace Chipsoft.Assignments.EPDConsole.Repositories
{
    internal class PhysicianRepository : IPhysicianRepository
    {
        private readonly EPDDbContext _context;
        public PhysicianRepository(EPDDbContext context)
        {
            _context = context;
        }

        public void AddPhysician(Physician physician)
        {
            _context.Physicians.Add(physician);
            _context.SaveChanges();
        }

        public IEnumerable<Patient> GetAllPhysicians()
        {
            return _context.Patients.ToList();
        }
    }
}
