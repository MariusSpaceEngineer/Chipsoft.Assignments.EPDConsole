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

        public void DeletePhysician(int id)
        {
            _context.Physicians.Remove(GetPhysicianById(id));
            _context.SaveChanges();
        }

        public IEnumerable<Physician> GetAllPhysicians()
        {
            return _context.Physicians.ToList();
        }

        public Physician GetPhysicianById(int id)
        {
            return _context.Physicians.FirstOrDefault(p => p.Id == id);
        }

        public Physician GetPhysicianByNameAndSpecialization(string name, string specialization)
        {
            return _context.Physicians.FirstOrDefault(p => p.Name == name && p.Specialization == specialization);
        }
    }
}
