using Chipsoft.Assignments.EPDConsole.Exceptions;
using Chipsoft.Assignments.EPDConsole.Models;
using Chipsoft.Assignments.EPDConsole.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Chipsoft.Assignments.EPDConsole
{
    public class Program
    {
        private readonly IPatientService _patientService;
        private readonly IPhysicianService _physicianService;
        private readonly IAppointmentService _appointmentService;

        private static ServiceProvider serviceProvider;

        public Program(IPatientService patientService, IPhysicianService physicianService, IAppointmentService appointmentService)
        {
            _patientService = patientService;
            _physicianService = physicianService;
            _appointmentService = appointmentService;
        }

        public bool ShowMenu()
        {
            Console.Clear();
            foreach (var line in File.ReadAllLines("logo.txt"))
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("");
            Console.WriteLine("1 - Patiënt toevoegen");
            Console.WriteLine("2 - Patiënten verwijderen");
            Console.WriteLine("3 - Arts toevoegen");
            Console.WriteLine("4 - Arts verwijderen");
            Console.WriteLine("5 - Afspraak toevoegen");
            Console.WriteLine("6 - Afspraken inzien");
            Console.WriteLine("7 - Sluiten");
            Console.WriteLine("8 - Reset db");
            Console.WriteLine("9 - Bekijk afspraken voor patiënt of arts");

            if (int.TryParse(Console.ReadLine(), out int option))
            {
                switch (option)
                {
                    case 1:
                        AddPatient();
                        return true;
                    case 2:
                        DeletePatient();
                        return true;
                    case 3:
                        AddPhysician();
                        return true;
                    case 4:
                        DeletePhysician();
                        return true;
                    case 5:
                        AddAppointment();
                        return true;
                    case 6:
                        ShowAppointments();
                        return true;
                    case 7:
                        return false;
                    case 8:
                        ResetDatabase();
                        return true;
                    case 9:
                        ShowAppointmentsForUser();
                        return true;
                    default:
                        return true;
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            // Setup DI
            var serviceCollection = new ServiceCollection();
            var startup = new Startup();
            startup.ConfigureServices(serviceCollection);

            serviceProvider = serviceCollection.BuildServiceProvider();
            var program = serviceProvider.GetRequiredService<Program>();

            while (program.ShowMenu())
            {
                // Continue
            }
        }

        private void AddPatient()
        {
            try
            {
                // Get patient details from user input and validate
                string name = ConsoleHelper.GetValidatedInput(
                    "Voer de naam van de patiënt in:",
                    ValidationHelper.IsValidName,
                    "Ongeldige invoer. Voer een geldig naam in.");
                if (name == null)
                {
                    return;
                }

                string address = ConsoleHelper.GetStringInput("Voer het adres van de patiënt in:");
                if (address == null)
                {
                    return;
                }

                string phoneNumber = ConsoleHelper.GetValidatedInput(
                    "Voer het telefoonnummer van de patiënt in:",
                    ValidationHelper.IsValidPhoneNumber,
                    "Ongeldige invoer. Voer een geldig telefoonnummer in.");
                if (phoneNumber == null)
                {
                    return;
                }

                string email = ConsoleHelper.GetValidatedInput(
                    "Voer het e-mailadres van de patiënt in:",
                    ValidationHelper.IsValidEmail,
                    "Ongeldige invoer. Voer een geldig e-mailadres in.");
                if (email == null)
                {
                    return;
                }

                var patient = new Patient
                {
                    Name = name,
                    Address = address,
                    PhoneNumber = phoneNumber,
                    Email = email
                };

                _patientService.AddPatient(patient);
                Console.WriteLine("Patiënt succesvol toegevoegd.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            catch (DuplicateEntityException)
            {
                Console.WriteLine("Er bestaat al een patiënt met dezelfde naam en telefoonnummer.");
            }
            catch (Exception)
            {
                Console.WriteLine($"Er is een onverwachte fout opgetreden.");
            }

            ConsoleHelper.PromptToContinue();
        }

        private void AddPhysician()
        {
            try
            {
                // Get physician details from user input and validate
                string name = ConsoleHelper.GetValidatedInput(
                    "Voer de naam van de arts in:",
                    ValidationHelper.IsValidName,
                    "Ongeldige invoer. Voer een geldig naam in."
                    );
                if (name == null)
                {
                    return;
                }

                string specialization = ConsoleHelper.GetStringInput("Voer de specialisatie van de arts in:");
                if (specialization == null)
                {
                    return;
                }

                var physician = new Physician
                {
                    Name = name,
                    Specialization = specialization
                };

                _physicianService.AddPhysician(physician);
                Console.WriteLine("Arts succesvol toegevoegd.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            catch (DuplicateEntityException)
            {
                Console.WriteLine("Er bestaat al een arts met dezelfde naam en specialisatie.");
            }
            catch (Exception)
            {
                Console.WriteLine($"Er is een onverwachte fout opgetreden.");
            }

            ConsoleHelper.PromptToContinue();
        }

        private void AddAppointment()
        {
            try
            {
                var patients = _patientService.GetAllPatients().ToList();
                var physicians = _physicianService.GetAllPhysicians().ToList();

                // Get user selection for patient and physician
                Patient selectedPatient = ConsoleHelper.GetSelectionInput("Kies een patiënt:",
                    patients,
                    p => p.Name,
                    p => p.Address,
                    p => p.Email,
                    p => p.PhoneNumber
                );

                if (selectedPatient == null)
                {
                    return;
                }

                Physician selectedPhysician = ConsoleHelper.GetSelectionInput("Kies een arts:",
                    physicians,
                    p => p.Name,
                    p => p.Specialization
                );

                if (selectedPhysician == null)
                {
                    return;
                }

                // Get user date input for appointment
                DateTime appointmentDate = ConsoleHelper.GetDateInput("Voer de datum van de afspraak in (dd/MM/yyyy HH:mm):", "dd/MM/yyyy HH:mm", true);
                if (appointmentDate == default)
                {
                    return;
                }

                // Get user input for appointment description (optional)
                string description = ConsoleHelper.GetStringInput("Voer een beschrijving van de afspraak in:", true);
                if (description == null)
                {
                    return;
                }

                var appointment = new Appointment
                {
                    PatientId = selectedPatient.Id,
                    PhysicianId = selectedPhysician.Id,
                    Date = appointmentDate,
                    Description = description
                };

                _appointmentService.AddAppointment(appointment);
                Console.WriteLine("Afspraak succesvol toegevoegd.");
                Console.Clear();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            catch (Exception)
            {
                Console.WriteLine($"Er is een onverwachte fout opgetreden.");
            }

            ConsoleHelper.PromptToContinue();
        }

        private void DeletePhysician()
        {
            try
            {
                var physicians = _physicianService.GetAllPhysicians().ToList();

                if (ConsoleHelper.IsListEmpty(physicians, "Er zijn geen artsen om te verwijderen."))
                {
                    return;
                }

                Physician selectedPhysician = ConsoleHelper.GetSelectionInput("Kies een arts om te verwijderen:",
                    physicians,
                    p => p.Name,
                    p => p.Specialization
                );

                if (selectedPhysician == null)
                {
                    return;
                }

                _physicianService.DeletePhysician(selectedPhysician.Id);
                Console.WriteLine("Arts succesvol verwijderd.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            catch (Exception)
            {
                Console.WriteLine($"Er is een onverwachte fout opgetreden.");
            }

            ConsoleHelper.PromptToContinue();
        }

        private void DeletePatient()
        {
            try
            {
                var patients = _patientService.GetAllPatients().ToList();

                if (ConsoleHelper.IsListEmpty(patients, "Er zijn geen patiënten om te verwijderen."))
                {
                    return;
                }

                Patient selectedPatient = ConsoleHelper.GetSelectionInput("Kies een patiënt om te verwijderen:",
                    patients,
                    p => p.Name,
                    p => p.Address,
                    p => p.Email,
                    p => p.PhoneNumber
                );

                if (selectedPatient == null)
                {
                    return;
                }

                _patientService.DeletePatient(selectedPatient.Id);
                Console.WriteLine("Patiënt succesvol verwijderd.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            catch (Exception)
            {
                Console.WriteLine($"Er is een onverwachte fout opgetreden.");
            }

            ConsoleHelper.PromptToContinue();
        }

        private void ShowAppointments()
        {
            var appointments = _appointmentService.GetAllAppointments();

            ConsoleHelper.ShowAppointmentDetails(appointments, "Afspraken:");

            ConsoleHelper.PromptToContinue();
        }

        private void ShowAppointmentsForUser()
        {
            // Ask user if they are a patient or a physician
            Console.Clear();
            Console.WriteLine("Bent u een patiënt of een arts?");
            Console.WriteLine("1 - Patiënt");
            Console.WriteLine("2 - Arts");

            if (int.TryParse(Console.ReadLine(), out int option))
            {
                switch (option)
                {
                    case 1:
                        ShowAppointmentsForPatient();
                        break;
                    case 2:
                        ShowAppointmentsForPhysician();
                        break;
                    default:
                        Console.WriteLine("Ongeldige optie.");
                        break;
                }
            }

        }

        private void ShowAppointmentsForPatient()
        {
            var patients = _patientService.GetAllPatients().ToList();

            if (ConsoleHelper.IsListEmpty(patients, "Er zijn geen patiënten om afspraken voor te bekijken."))
            {
                return;
            }

            Patient selectedPatient = ConsoleHelper.GetSelectionInput(
                "Kies een patiënt:",
                patients,
                p => p.Name,
                p => p.Address,
                p => p.Email,
                p => p.PhoneNumber
            );

            if (selectedPatient == null)
            {
                return;
            }

            var appointments = _appointmentService.GetAppointmentsByPatientId(selectedPatient.Id);

            ConsoleHelper.ShowAppointmentDetails(appointments, $"Afspraken voor patiënt {selectedPatient.Name}:");

            ConsoleHelper.PromptToContinue();
        }

        private void ShowAppointmentsForPhysician()
        {
            var physicians = _physicianService.GetAllPhysicians().ToList();

            if (ConsoleHelper.IsListEmpty(physicians, "Er zijn geen artsen om afspraken voor te bekijken."))
            {
                return;
            }

            Physician selectedPhysician = ConsoleHelper.GetSelectionInput("Kies een arts:",
                physicians,
                p => p.Name,
                p => p.Specialization
                );

            if (selectedPhysician == null)
            {
                return;
            }

            var appointments = _appointmentService.GetAppointmentsByPhysicianId(selectedPhysician.Id);

            ConsoleHelper.ShowAppointmentDetails(appointments, $"Afspraken voor arts {selectedPhysician.Name}:");
        }

        private void ResetDatabase()
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<EPDDbContext>();
                    dbContext.Database.EnsureDeleted();
                    dbContext.Database.EnsureCreated();
                }
                Console.Clear();
                Console.WriteLine("Database succesvol gereset.");
            }
            catch (Exception)
            {
                Console.WriteLine($"Fout bij het resetten van de database.");
            }

            ConsoleHelper.PromptToContinue();
        }

        
    }
}



