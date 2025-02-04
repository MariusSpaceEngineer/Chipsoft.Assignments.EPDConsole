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

        //Get all appointments with patient and physician details
        public IEnumerable<Appointment> GetAll()
        {
            return _context.Appointments
                            .Include(a => a.Patient)
                            .Include(a => a.Physician)
                            .ToList();
        }

        //Get all appointments for a patient with patient and physician details
        public IEnumerable<Appointment> GetByPatientId(int patientId)
        {
            return _context.Appointments
                            .Include(a => a.Patient)
                            .Include(a => a.Physician)
                            .Where(a => a.PatientId == patientId)
                            .ToList();
        }

        //Get all appointments for a physician with patient and physician details
        public IEnumerable<Appointment> GetByPhysicianId(int physicianId)
        {
            return _context.Appointments
                            .Include(a => a.Patient)
                            .Include(a => a.Physician)
                            .Where(a => a.PhysicianId == physicianId)
                            .ToList();
        }
    }
}
