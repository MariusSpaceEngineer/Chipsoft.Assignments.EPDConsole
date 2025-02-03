using Chipsoft.Assignments.EPDConsole.Repositories;
using Chipsoft.Assignments.EPDConsole.Repositories.Interfaces;
using Chipsoft.Assignments.EPDConsole.Services;
using Chipsoft.Assignments.EPDConsole.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chipsoft.Assignments.EPDConsole
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EPDDbContext>(options => options.UseSqlite("Data Source=epd.db"));
            services.AddScoped<IAppointmentRespository, AppointmentRepository>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IPhysicianRepository, PhysicianRepository>();
            services.AddScoped<IPhysicianService, PhysicianService>();
            services.AddTransient<Program>();
        }
    }
}
