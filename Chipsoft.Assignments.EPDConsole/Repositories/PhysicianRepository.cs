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

        public void Add(Physician physician)
        {
            _context.Physicians.Add(physician);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Physicians.Remove(GetById(id));
            _context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _context.Physicians.Any(p => p.Id == id);
        }

        public IEnumerable<Physician> GetAll()
        {
            return _context.Physicians.ToList();
        }

        public Physician GetById(int id)
        {
            return _context.Physicians.FirstOrDefault(p => p.Id == id);
        }

        public Physician GetByNameAndSpecialization(string name, string specialization)
        {
            return _context.Physicians.FirstOrDefault(p => p.Name == name && p.Specialization == specialization);
        }
    }
}
