using Chipsoft.Assignments.EPDConsole.Models;
using Chipsoft.Assignments.EPDConsole.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace Chipsoft.Assignments.EPDConsole
{
    public class Program
    {
        private readonly IPatientService _patientService;
        private readonly IPhysicianService _physicianService;
        private readonly IAppointmentService _appointmentService;

        public Program(IPatientService patientService, IPhysicianService physicianService, IAppointmentService appointmentService)
        {
            _patientService = patientService;
            _physicianService = physicianService;
            _appointmentService = appointmentService;
        }

        private void AddPatient()
        {
            try
            {
                string name;
                string address;
                string phoneNumber;
                string email;

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Voer de naam van de patiënt in:");
                    name = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige invoer. De naam mag niet leeg zijn.");
                        Console.WriteLine("Druk op een toets om opnieuw te proberen...");
                        Console.ReadKey();
                    }
                }

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Voer het adres van de patiënt in:");
                    address = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(address))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige invoer. Het adres mag niet leeg zijn.");
                        Console.WriteLine("Druk op een toets om opnieuw te proberen...");
                        Console.ReadKey();
                    }
                }

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Voer het telefoonnummer van de patiënt in:");
                    phoneNumber = Console.ReadLine()?.Trim();

                    if (!string.IsNullOrWhiteSpace(phoneNumber) && IsValidPhoneNumber(phoneNumber))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige invoer. Voer een geldig telefoonnummer in.");
                        Console.WriteLine("Druk op een toets om opnieuw te proberen...");
                        Console.ReadKey();
                    }
                }

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Voer het e-mailadres van de patiënt in:");
                    email = Console.ReadLine()?.Trim();

                    if (!string.IsNullOrWhiteSpace(email) && IsValidEmail(email))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige invoer. Voer een geldig e-mailadres in.");
                        Console.WriteLine("Druk op een toets om opnieuw te proberen...");
                        Console.ReadKey();
                    }
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
                Console.WriteLine($"Fout: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Er is een onverwachte fout opgetreden: {ex.Message}");
            }

            Console.WriteLine("Druk op een toets om terug te keren naar het menu...");
            Console.ReadKey();
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            var phoneRegex = new System.Text.RegularExpressions.Regex(@"^\+?[1-9]\d{1,14}$");
            return phoneRegex.IsMatch(phoneNumber);
        }

        private bool IsValidEmail(string email)
        {
            var emailAttribute = new EmailAddressAttribute();
            return emailAttribute.IsValid(email);
        }


        private void ShowAppointment()
        {
            var appointments = _appointmentService.GetAllAppointments();

            Console.Clear();
            Console.WriteLine("Afspraken:");
            foreach (var appointment in appointments)
            {
                Console.WriteLine($"Patiënt: {appointment.Patient.Name} - Arts: {appointment.Physician.Name} - Datum: {appointment.Date}");
            }

            Console.WriteLine("");
            Console.WriteLine("Druk op een toets om terug te keren naar het menu...");
            Console.ReadKey();
        }

        private void AddAppointment()
        {
            try
            {
                var patients = _patientService.GetAllPatients().ToList();
                var physicians = _physicianService.GetAllPhysicians().ToList();

                Patient? selectedPatient = null;
                while (selectedPatient == null)
                {
                    Console.Clear();
                    Console.WriteLine("Kies een patiënt:");
                    for (int i = 0; i < patients.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} - {patients[i].Name}");
                    }

                    if (int.TryParse(Console.ReadLine(), out int patientIndex) && patientIndex >= 1 && patientIndex <= patients.Count)
                    {
                        selectedPatient = patients[patientIndex - 1];
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige invoer. Probeer het opnieuw.");
                        Console.WriteLine("Druk op een toets om opnieuw te proberen...");
                        Console.ReadKey();
                    }
                }

                Physician? selectedPhysician = null;
                while (selectedPhysician == null)
                {
                    Console.Clear();
                    Console.WriteLine("Kies een arts:");
                    for (int i = 0; i < physicians.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} - {physicians[i].Name}");
                    }

                    if (int.TryParse(Console.ReadLine(), out int physicianIndex) && physicianIndex >= 1 && physicianIndex <= physicians.Count)
                    {
                        selectedPhysician = physicians[physicianIndex - 1];
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige invoer. Probeer het opnieuw.");
                        Console.WriteLine("Druk op een toets om opnieuw te proberen...");
                        Console.ReadKey();
                    }
                }

                DateTime appointmentDate;
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Voer de datum van de afspraak in (dd/MM/yyyy HH:mm):");
                    if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out appointmentDate))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige datum. Probeer het opnieuw.");
                        Console.WriteLine("Druk op een toets om opnieuw te proberen...");
                        Console.ReadKey();
                    }
                }

                Console.Clear();
                Console.WriteLine("Voer een beschrijving van de afspraak in:");
                var description = Console.ReadLine();

                var appointment = new Appointment
                {
                    PatientId = selectedPatient.Id,
                    PhysicianId = selectedPhysician.Id,
                    Date = appointmentDate,
                    Description = description
                };

                _appointmentService.AddAppointment(appointment);

                Console.WriteLine("Afspraak succesvol toegevoegd.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Fout: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Er is een onverwachte fout opgetreden: {ex.Message}");
            }

            Console.WriteLine("Druk op een toets om terug te keren naar het menu...");
            Console.ReadKey();
        }

        private void DeletePhysician()
        {
            try
            {
                var physicians = _physicianService.GetAllPhysicians().ToList();

                Physician? selectedPhysician = null;
                while (selectedPhysician == null)
                {
                    Console.Clear();
                    Console.WriteLine("Kies een arts om te verwijderen:");
                    for (int i = 0; i < physicians.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} - {physicians[i].Name}");
                    }

                    if (int.TryParse(Console.ReadLine(), out int physicianIndex) && physicianIndex >= 1 && physicianIndex <= physicians.Count)
                    {
                        selectedPhysician = physicians[physicianIndex - 1];
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige invoer. Probeer het opnieuw.");
                        Console.WriteLine("Druk op een toets om opnieuw te proberen...");
                        Console.ReadKey();
                    }
                }

                _physicianService.DeletePhysician(selectedPhysician.Id);
                Console.WriteLine("Arts succesvol verwijderd.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Fout: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Er is een onverwachte fout opgetreden: {ex.Message}");
            }

            Console.WriteLine("Druk op een toets om terug te keren naar het menu...");
            Console.ReadKey();
        }

        private void AddPhysician()
        {
            try
            {
                string name;
                string specialization;

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Voer de naam van de arts in:");
                    name = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige invoer. De naam mag niet leeg zijn.");
                        Console.WriteLine("Druk op een toets om opnieuw te proberen...");
                        Console.ReadKey();
                    }
                }

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Voer de specialisatie van de arts in:");
                    specialization = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(specialization))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige invoer. De specialisatie mag niet leeg zijn.");
                        Console.WriteLine("Druk op een toets om opnieuw te proberen...");
                        Console.ReadKey();
                    }
                }

                var physician = new Physician
                {
                    Name = name,
                    Specialization = specialization
                };

                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(physician);
                if (!Validator.TryValidateObject(physician, validationContext, validationResults, true))
                {
                    var errors = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                    throw new ArgumentException($"Model validation failed: {errors}");
                }

                _physicianService.AddPhysician(physician);
                Console.WriteLine("Arts succesvol toegevoegd.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Fout: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Er is een onverwachte fout opgetreden: {ex.Message}");
            }

            Console.WriteLine("Druk op een toets om terug te keren naar het menu...");
            Console.ReadKey();
        }

        private void DeletePatient()
        {
            try
            {
                var patients = _patientService.GetAllPatients().ToList();

                Patient? selectedPatient = null;
                while (selectedPatient == null)
                {
                    Console.Clear();
                    Console.WriteLine("Kies een patiënt om te verwijderen:");
                    for (int i = 0; i < patients.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} - {patients[i].Name}");
                    }

                    if (int.TryParse(Console.ReadLine(), out int patientIndex) && patientIndex >= 1 && patientIndex <= patients.Count)
                    {
                        selectedPatient = patients[patientIndex - 1];
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige invoer. Probeer het opnieuw.");
                        Console.WriteLine("Druk op een toets om opnieuw te proberen...");
                        Console.ReadKey();
                    }
                }

                _patientService.DeletePatient(selectedPatient.Id);
                Console.WriteLine("Patiënt succesvol verwijderd.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Fout: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Er is een onverwachte fout opgetreden: {ex.Message}");
            }

            Console.WriteLine("Druk op een toets om terug te keren naar het menu...");
            Console.ReadKey();
        }

        #region FreeCodeForAssignment
        private static ServiceProvider serviceProvider;

        static void Main(string[] args)
        {
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

        public bool ShowMenu()
        {
            Console.Clear();
            foreach (var line in File.ReadAllLines("logo.txt"))
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("");
            Console.WriteLine("1 - Patient toevoegen");
            Console.WriteLine("2 - Patienten verwijderen");
            Console.WriteLine("3 - Arts toevoegen");
            Console.WriteLine("4 - Arts verwijderen");
            Console.WriteLine("5 - Afspraak toevoegen");
            Console.WriteLine("6 - Afspraken inzien");
            Console.WriteLine("7 - Sluiten");
            Console.WriteLine("8 - Reset db");

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
                        ShowAppointment();
                        return true;
                    case 7:
                        return false;
                    case 8:
                        using (var scope = serviceProvider.CreateScope())
                        {
                            var dbContext = scope.ServiceProvider.GetRequiredService<EPDDbContext>();
                            dbContext.Database.EnsureDeleted();
                            dbContext.Database.EnsureCreated();
                        }
                        return true;
                    default:
                        return true;
                }
            }
            return true;
        }
        #endregion
    }
}
