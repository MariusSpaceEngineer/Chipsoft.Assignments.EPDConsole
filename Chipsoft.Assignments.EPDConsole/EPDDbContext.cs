using Chipsoft.Assignments.EPDConsole.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPDConsole
{
    public class EPDDbContext : DbContext
    {
        // The following configures EF to create a Sqlite database file in the
        public EPDDbContext(DbContextOptions<EPDDbContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Physician> Physicians { get; set; }
        public DbSet<Appointment> Appointments { get; set; }  
    }
}
