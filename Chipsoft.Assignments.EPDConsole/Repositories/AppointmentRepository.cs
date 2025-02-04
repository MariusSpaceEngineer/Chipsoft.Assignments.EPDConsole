using Chipsoft.Assignments.EPDConsole.Models;
using Chipsoft.Assignments.EPDConsole.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chipsoft.Assignments.EPDConsole.Repositories
{
    internal class AppointmentRepository : IAppointmentRespository
    {
        private readonly EPDDbContext _context;
        public AppointmentRepository(EPDDbContext context)
        {
            _context = context;
        }

        public void Add(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _context.Appointments
                            .Include(a => a.Patient)
                            .Include(a => a.Physician)
                            .ToList();
        }
    }
}
