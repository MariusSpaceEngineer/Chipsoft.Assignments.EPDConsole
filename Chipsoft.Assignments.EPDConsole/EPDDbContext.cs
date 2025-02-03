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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasData(
                new Patient { Id = 1, Name = "John Doe", Address = "123 Main St", PhoneNumber = "555-1234", Email = "john.doe@example.com" },
                new Patient { Id = 2, Name = "Jane Smith", Address = "456 Elm St", PhoneNumber = "555-5678", Email = "jane.smith@example.com" }
            );

            modelBuilder.Entity<Physician>().HasData(
                new Physician { Id = 1, Name = "Dr. Alice Brown", Specialization = "Cardiology" },
                new Physician { Id = 2, Name = "Dr. Bob White", Specialization = "Neurology" }
            );

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment { Id = 1, PatientId = 1, PhysicianId = 1, Date = new DateTime(2023, 10, 1, 9, 0, 0), Description = "Routine check-up" },
                new Appointment { Id = 2, PatientId = 2, PhysicianId = 2, Date = new DateTime(2023, 10, 2, 10, 0, 0), Description = "Consultation" }
            );
        }
    }
}
